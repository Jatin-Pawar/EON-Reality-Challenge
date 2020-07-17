using UnityEngine;

public class SceneController : MonoBehaviour
{

    [SerializeField]
    private int score = 0;

    [SerializeField]
    private Transform platform;

    [SerializeField]
    float circleRadius = 0.0f;

    [SerializeField]
    [Range(0,360)]
    float angle = 0.0f;

    [SerializeField]
    private GameObject Enemy;

    [SerializeField]
    private float currentEnemyCount = 0;

    [SerializeField]
    private float maxEnemyCount = 10;

    [SerializeField]
    Vector3 pointOnCircle = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        circleRadius = platform.localScale.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            spawnEnemy();
        }
    }

    private void spawnEnemy()
    {
        if (currentEnemyCount <maxEnemyCount)
        {
            //getting a random position on circle
            angle = Random.Range(0,360);
            pointOnCircle.x = circleRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            pointOnCircle.z = circleRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
            pointOnCircle.y = platform.localScale.y * 2 + Enemy.transform.localScale.y/2;

            //check if we already have any enemy on the point

            //if no enemy on the position then we can spawn an enemy            
            Instantiate(Enemy, pointOnCircle, Quaternion.identity);

            //increment current enemy count
            currentEnemyCount++;

        }
        

    }

    public void UpdateScore()
    {
        score = score + 1;
    }

    public void UpdateEnemyCount()
    {
        currentEnemyCount = currentEnemyCount - 1;
    }
}
