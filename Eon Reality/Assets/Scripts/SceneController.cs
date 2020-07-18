using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Exposed Variable
    [SerializeField]
    private Transform platform;

    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private float maxEnemyCount = 10;

    [SerializeField]
    private TextMeshProUGUI timerUI;

    [SerializeField]
    private TextMeshProUGUI scoreUI;

    [SerializeField]
    private TextMeshProUGUI finaScoreUI;

    [SerializeField]
    private GameObject gameOverUI;

    #endregion

    #region Non Exposed Variables

    private float circleRadius = 0.0f;

    private int score = 0;

    private float timer = 60.0f;          

    [Range(0,360)]
    private float angle = 0.0f;

    private float currentEnemyCount = 0;

    private Vector3 pointOnCircle = Vector3.zero;

    List<float> nonEnemyList = new List<float>();

    List<float> enemyList = new List<float>();

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        circleRadius = platform.localScale.x / 2;

        StartCoroutine(GameTimer());

        //Initialize list....
        for (int i =0;i<360;i++)
        {
            nonEnemyList.Add(i);
        }
    }

    void Update()
    {
        if (currentEnemyCount < maxEnemyCount)
        {
            spawnEnemy();
        }

        if (timer <= 0)
        {
            onGameOver();
        }

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetGame();
        }
    }

    // Select a random point on the edge and instantiate an enemy
    // Improved logic so as to keep the randomization at max
    private void spawnEnemy()
    {
        //Selecting a random angle
        angle = Random.Range(0, 360);

        float radianAngle;
        //If no enemy is spawned yet...then spawn an enemy
        if (enemyList.Count == 0)
            {
                //Debug.Log("Enenmy can be spawned at " + angle);
                enemyList.Add(angle);
                nonEnemyList.Remove(angle);

                radianAngle = Mathf.Deg2Rad * angle;

                //Getting the position based on angle
                pointOnCircle.x = circleRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
                pointOnCircle.z = circleRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
                pointOnCircle.y = platform.localScale.y * 2 + Enemy.transform.localScale.y / 2;

                //Instantiating enemy at the given position
                Instantiate(Enemy, pointOnCircle, Quaternion.identity);

                // Increment the enemy count
                currentEnemyCount++;

                return;

        }//check if we can spawn an enemy here so that it will not collide
        else
        {                
                bool flag = true;
                for (int i = 0; i < enemyList.Count; i++)
                {
                    if ((Mathf.Abs(enemyList[i] - angle) < 5))
                    {
                        flag = false;
                    //Debug.Log("Enenmy cannot be spawned at " + angle + " as it will collide");
                    return;
                    }
                }

                if (flag)
                {
                    //Debug.Log("Enenmy can be spawned at " + angle + " as it will not collide");
                    enemyList.Add(angle);
                    nonEnemyList.Remove(angle);

                    radianAngle = Mathf.Deg2Rad * angle;
                    Debug.Log("Deg@Rad: " + radianAngle);

                    //Getting the position based on angle
                    pointOnCircle.x = circleRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
                    pointOnCircle.z = circleRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
                    pointOnCircle.y = platform.localScale.y * 2 + Enemy.transform.localScale.y / 2;

                    //Instantiating enemy at the given position
                    Instantiate(Enemy, pointOnCircle, Quaternion.identity);

                    // Increment the enemy count
                    currentEnemyCount++;

                }
        }  
    }

    // Uodates the Score
    public void UpdateScore()
    {        
        score = score + 1;
        scoreUI.text = $"{"Score:" + score}";
    }

    // Things to be taken care of when an enemy dies
    public void onEnemyDied(Transform enemyTransform)
    {
        //Get the angle from position
        float radianAngle = Mathf.Atan2(enemyTransform.localPosition.z, enemyTransform.localPosition.x);

        float degAngle = radianAngle * Mathf.Rad2Deg;

        //Debug.Log("Before Deg: " + degAngle);
        if(degAngle < 0)
        {
            degAngle = degAngle + 360;
        }
        //Debug.Log("After Deg: " + degAngle);

        //Updating the lists.....
        enemyList.Remove(degAngle);
        nonEnemyList.Add(degAngle);

        //Decreament enenyCount
        currentEnemyCount = currentEnemyCount - 1;
    }

    // Game Time Counter
    public IEnumerator GameTimer()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timer--;
            timerUI.text = $"{"Timer: " + timer }";
        }
    }

    // Takes care of game over
    void onGameOver()
    {
        finaScoreUI.text = $"{"Final Score:" + score}";
        gameOverUI.SetActive(true);
    }

    // Used to Reset the Game
    void ResetGame()
    {
        SceneManager.LoadScene(0);

    }
}
