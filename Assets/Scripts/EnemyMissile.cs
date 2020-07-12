using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject missilePrefab;
    GameObject[] defenders;

    private GameController myGameController;

    Vector3 target;
    float newspeed;

    private float randomTimer;
    public Vector3 StartingLocation { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = GetTarget();
        newspeed = Random.Range(GameController.enemySpeedToUse/2.0f, GameController.enemySpeedToUse);
            //CompensateSpeed(GameController.enemySpeedToUse);
        Debug.Log(GameController.enemySpeedToUse);

        Vector3 targetXPosition = new Vector3(this.target.x, StartingLocation.y, 0.0f);
        this.transform.position = Vector3.Lerp(StartingLocation, targetXPosition, 0.5f);

        speed = myGameController.enemyMissileSpeed;

        randomTimer = Random.Range(0.1f, 50f);
        Invoke("SplitMissile", randomTimer);
    }

    private Vector3 GetTarget()
    {
        GameObject defender;
        while (true)
        {
            defender = defenders[Random.Range(0, defenders.Length)];
            bool isDestroyed = defender.GetComponent<DestroyController>().IsDestroyed;
            if (!isDestroyed)
                break;
        }
        return defender.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, newspeed * Time.deltaTime);

        Vector3 dir = (target - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle + 90);
        transform.rotation = rotation;

        if (transform.position == target)
        {
            myGameController.EnemyMissileDestroyed();
            MissileExplode();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Defenders")
        {
            CasaController casaController = collision.gameObject.GetComponent<CasaController>();
            casaController.SetMorto();
            DestroyController destroyController = collision.gameObject.GetComponent<DestroyController>();
            destroyController.IsDestroyed = true;
            
            myGameController.EnemyMissileDestroyed();
            MissileExplode();
            if(collision.GetComponent<MissileLauncher>() != null)
            {
                myGameController.MissileLauncherHit();
                return;
            }
            myGameController.cityCounter--;
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Explosion")
        {
            myGameController.AddMissileDestroyedScore();
            MissileExplode();
            //Destroy(collision.gameObject);
        }
    }

    private float CompensateSpeed(float input)
    {
        Vector3 velocity = (target - this.transform.position).normalized * input;
        Vector3 velocityDown = Vector3.Project(velocity, Vector3.down);
        return input / velocityDown.magnitude;
    }

    private void MissileExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void SplitMissil()
    {
        float yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, -25f, 0)).y;
        if(transform.position.y >= yValue)
        {
            myGameController.enemyMissilesLeft++;
            Instantiate(missilePrefab, transform.position, Quaternion.identity);
        }

    }
}
