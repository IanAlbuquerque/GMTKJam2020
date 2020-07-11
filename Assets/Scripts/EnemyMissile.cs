using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] defenders;

    private GameController myGameController;

    Vector3 target;
    float newspeed;
    

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0, defenders.Length)].transform.position;
        newspeed = CompensateSpeed(speed);

        speed = myGameController.enemyMissileSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, newspeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Defenders")
        {
            myGameController.EnemyMissileDestroyed();
            MissileExplode();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Explosion")
        {
            myGameController.AddMissileDestroyedScore();
            MissileExplode();
            //Destroy(collision.gameObject);
        }
    }

    private float CompensateSpeed(float speed)
    {
        Vector3 velocity = (target - this.transform.position).normalized * speed;
        Vector3 velocityDown = Vector3.Project(velocity, Vector3.down);
        return speed / velocityDown.magnitude;
    }

    private void MissileExplode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
