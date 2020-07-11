using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    EnemyMissileSpawner myEnemyMissileSpawner;

    public int score = 0;
    public int level = 1;
    public int playerMissilesLeft = 30;
    private int enemyMissilesThisRound = 20;
    private int enemyMissilesLeft = 0;

    //Score values
    private int missileDestroyedPoints = 25;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myMissileLeftText;

    // Start is called before the first frame update
    void Start()
    {
        myEnemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();

        UpdateScoreText();
        UpdateLevelText();
        UpdateMissileLeftText();

        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMissileLeftText()
    {
        myMissileLeftText.text = "Missiles Left: " + playerMissilesLeft;
    }
    public void UpdateLevelText()
    {
        myLevelText.text = "Level: " + level;
    }
    public void UpdateScoreText()
    {
        myScoreText.text = "Score: " + score;
    }

    public void AddMissileDestroyedScore()
    {
        score += missileDestroyedPoints;
        UpdateScoreText();
    }

    public void StartRound()
    {
        myEnemyMissileSpawner.missilesToSpawnThisRound = enemyMissilesThisRound;
        myEnemyMissileSpawner.StartRound();
    }

}
