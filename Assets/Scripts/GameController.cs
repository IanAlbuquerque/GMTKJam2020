using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    EnemyMissileSpawner myEnemyMissileSpawner;
    selfDestroy explosionTime;

    public static float speedMod = 0;

    public static float explosionMod = 0f;

    [SerializeField] private GameObject EndPainel;

    public int score = 0;
    public int level = 1;

    public int cityCounter = 6;

    public float enemyMissileSpeed = 5f;
    [SerializeField] private AnimationCurve enemyMissileSpeedMultiplierCurve;

    public int currentMissilesLoaded = 0;
    public int playerMissilesLeft;
    public int enemyMissilesThisRound = 20;
    public int enemyMissilesLeft = 0;
    [SerializeField] private int missileEndOfRound = 5;
    [SerializeField] private int citiesEndOfRound = 100;

    //mods
    float delay;

    int maxAmmo;



    //Score values
    private int missileDestroyedPoints = 20;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myMissileLeftText;
    [SerializeField] private TextMeshProUGUI currentMissilesLoadedLeftText;

    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private TextMeshProUGUI missileBonusText;
    [SerializeField] private TextMeshProUGUI citiesBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;

    GameObject[] Casa;
    private bool RoundisOver = false;

    public static bool IsPaused = false;

    public static float enemySpeedToUse;

    // Start is called before the first frame update
    void Start()
    {
        maxAmmo = this.playerMissilesLeft;
        currentMissilesLoaded = 10;
        playerMissilesLeft -= 10;

        myEnemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();
        //myPlayerController = GetComponent<PlayerMissileController>();
        
        GameController.enemySpeedToUse = this.enemyMissileSpeed;

        UpdateScoreText();
        UpdateLevelText();
        UpdateMissileLeftText();
        UpdatecurrentMissileLoadedText();

        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyMissilesLeft <= 0 && !RoundisOver )
        {
            Debug.Log("Cabou o jogo");
            RoundisOver = true;
            StartCoroutine(EndofRound());
        }
        if(cityCounter <= 0)
        {
            SceneManager.LoadScene("The End");
        }
    }

    public void AddMissileAmmo()
    {
        GameController.IsPaused = false;
        maxAmmo += 2;
        Time.timeScale = 1;
        EndPainel.SetActive(false);
        PrepareRound();
    }

    public void PlayerAddSpeed()
    {
        GameController.IsPaused = false;
        GameController.speedMod += 1;
        Time.timeScale = 1;
        EndPainel.SetActive(false);
        PrepareRound();
    }

    public void DelayMonsters()
    {
        GameController.IsPaused = false;
        delay = 0.2f;
        myEnemyMissileSpawner.delayBetweenMissiles += delay;
        Time.timeScale = 1;
        EndPainel.SetActive(false);
        PrepareRound();
    }

    public void GreaterExplosion()
    {
        GameController.IsPaused = false;
        GameController.explosionMod += .2f;
        Time.timeScale = 1;
        EndPainel.SetActive(false);
        PrepareRound();
    }

    public void UpdateMissileLeftText()
    {
        myMissileLeftText.text = playerMissilesLeft.ToString();
        UpdatecurrentMissileLoadedText();
    }

    public void UpdateLevelText()
    {
        myLevelText.text = "Level: " + level;
    }

    public void UpdatecurrentMissileLoadedText()
    {
        currentMissilesLoadedLeftText.text = (currentMissilesLoaded + playerMissilesLeft).ToString();
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

    public void PlayerFiredMissile()
    {
        if(currentMissilesLoaded > 0)
        {
            currentMissilesLoaded--;
        }
        if(currentMissilesLoaded == 0)
        {
            if (playerMissilesLeft >= 10)
            {
                currentMissilesLoaded = 10;
                playerMissilesLeft -= 10;
            }
            else
            {
                currentMissilesLoaded = playerMissilesLeft;
                playerMissilesLeft = 0;
            }
        }

        UpdateMissileLeftText();
    }

    public void MissileLauncherHit()
    {
        //playerMissilesLeft -= 10;

        if(playerMissilesLeft >= 10)
        {
            currentMissilesLoaded = 10;
            playerMissilesLeft -= 10;
        }
        else
        {
            currentMissilesLoaded = playerMissilesLeft;
            playerMissilesLeft = 0;
        }
        UpdateMissileLeftText();
        UpdatecurrentMissileLoadedText();
    }

    public void StartRound()
    {
        myEnemyMissileSpawner.missilesToSpawnThisRound = enemyMissilesThisRound;
        enemyMissilesLeft = enemyMissilesThisRound;
        myEnemyMissileSpawner.StartRound();
    }

    public IEnumerator EndofRound()
    {
        yield return new WaitForSeconds(.5f);
        GameController.IsPaused = true;
        EndPainel.SetActive(true);
        Time.timeScale = 0;
        int missileBonus = (playerMissilesLeft + currentMissilesLoaded) + missileEndOfRound;

        CasaController[] casas = GameObject.FindObjectsOfType<CasaController>();
        int cityBonus = casas.Length * citiesEndOfRound;

        int totalBonus = missileBonus * cityBonus;

        if (level >= 3 && level < 5)
        {
            totalBonus *= 1;
        }

        else if (level >= 5 && level < 7)
        {
            totalBonus *= 2;
        }

        else if (level >= 7 && level < 9)
        {
            totalBonus *= 3;
        }

        else if (level >= 9 && level < 11)
        {
            totalBonus *= 4;
        }

        else if (level >= 11)
        {
            totalBonus *= 5;
        }


        missileBonusText.text = "Left Missile Bonus: " + missileBonus;
        citiesBonusText.text = "Left Cities Bonus: " + cityBonus;
        totalBonusText.text = "Total Score:" + totalBonus;

        score += totalBonus;
        UpdateScoreText();

        /*countdownText.text = "8";
        yield return new WaitForSeconds(1f);
        countdownText.text = "7";
        yield return new WaitForSeconds(1f);
        countdownText.text = "6";
        yield return new WaitForSeconds(1f);
        countdownText.text = "5";
        yield return new WaitForSeconds(1f);
        countdownText.text = "4";
        yield return new WaitForSeconds(1f);
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);
        countdownText.text = "2";
        yield return new WaitForSeconds(1f);
        countdownText.text = "1";
        yield return new WaitForSeconds(1f);*/
    }

    private void PrepareRound()
    {
        RoundisOver = false;

        //new round setting
        playerMissilesLeft = maxAmmo;
        enemyMissileSpeed += this.enemyMissileSpeedMultiplierCurve.Evaluate(level);
        GameController.enemySpeedToUse = this.enemyMissileSpeed;

        currentMissilesLoaded = 10;
        playerMissilesLeft -= 10;
        enemyMissilesThisRound += 2;

        StartRound();
        level++;
        UpdateLevelText();
        UpdateMissileLeftText();

    }

}
