using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject explosionPrefab;
    GameObject[] defenders;

    Vector3 target;
    float newspeed;
    

    // Start is called before the first frame update
    void Start()
    {
        defenders = GameObject.FindGameObjectsWithTag("Defenders");
        target = defenders[Random.Range(0, defenders.Length)].transform.position;
        newspeed = CompensateSpeed(speed);
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
            MissileExplode();
            Destroy(collision.gameObject);
        }
        else if (collision.tag == "Explosion")
        {
            FindObjectOfType<GameController>().AddMissileDestroyedScore();
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
