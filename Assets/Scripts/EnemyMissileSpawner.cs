using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private float Yscreen = 0.5f;

    private float minX, maxX;

    public int missilesToSpawnThisRound = 10;
    public float delayBetweenMissiles = .5f;

    float yValue;

    // Start is called before the first frame update
    void Awake()
    {
        minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).x;
        maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;

        float randomX = Random.Range(minX, maxX);
        yValue = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

        //StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRound()
    {
        StartCoroutine(SpawnMissiles());
    }
   
    public IEnumerator SpawnMissiles()
    {
        while (missilesToSpawnThisRound > 0)
        {
            float randomX = Random.Range(minX, maxX);

            Vector3 dir = missilePrefab.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 spawnPosition = new Vector3(randomX, yValue + Yscreen, 0);
            GameObject missile = Instantiate(missilePrefab, new Vector3(randomX, yValue + Yscreen, 0), Quaternion.identity);
            EnemyMissile enemyMissile = missile.GetComponent<EnemyMissile>();
            enemyMissile.StartingLocation = spawnPosition;

            missilesToSpawnThisRound--;

            yield return new WaitForSeconds(delayBetweenMissiles);
        }
    }
}
