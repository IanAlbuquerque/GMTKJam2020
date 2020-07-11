using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    EnemyMissileSpawner myEnemyMissileSpawner;

    [SerializeField] private GameObject EndPainel;

    public int score = 0;
    public int level = 1;
    public int playerMissilesLeft = 30;
    private int enemyMissilesThisRound = 20;
    private int enemyMissilesLeft = 0;
    [SerializeField] private int missileEndOfRound = 5;
    [SerializeField] private int citiesEndOfRound = 100;

    //Score values
    private int missileDestroyedPoints = 25;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myMissileLeftText;

    [SerializeField] private TextMeshProUGUI missileBonusText;
    [SerializeField] private TextMeshProUGUI citiesBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;

    GameObject[] Casa;

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
        /* if(enemyMissilesLeft <= 0)
        {
            Debug.Log("Cabou o jogo");
            StartCoroutine(EndofRound());
        }*/
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
        EnemyMissileDestroyed();
        UpdateScoreText();
    }

    public void EnemyMissileDestroyed()
    {
        enemyMissilesLeft--;
    }

    public void StartRound()
    {
        myEnemyMissileSpawner.missilesToSpawnThisRound = enemyMissilesThisRound;
        myEnemyMissileSpawner.StartRound();
    }

    public IEnumerator EndofRound()
    {
        yield return new WaitForSeconds(.5f);
        EndPainel.SetActive(true);
        int missileBonus = playerMissilesLeft * missileEndOfRound;

        CasaController[] casas = GameObject.FindObjectsOfType<CasaController>();
        int cityBonus = casas.Length * citiesEndOfRound;

        int totalBonus = missileBonus * cityBonus;

        missileBonusText.text = "Left Missile Bonus: " + missileBonus;
        citiesBonusText.text = "Left Cities Bonus: " + cityBonus;
        totalBonusText.text = "Total Score:" + totalBonus;

        score += totalBonus;
    }

}
