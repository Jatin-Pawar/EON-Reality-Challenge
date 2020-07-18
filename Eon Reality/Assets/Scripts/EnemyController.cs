using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    Color[] enemyColor = {Color.black,Color.blue,Color.yellow,Color.red,Color.green,Color.grey,Color.magenta};

    [SerializeField]
    private float changeColorTime = 2.0f;

    private float colorTimer = 0.0f;   


    void Start()
    {
        transform.GetComponent<MeshRenderer>().material.color = enemyColor[Random.Range(0,enemyColor.Length)];      
    }

    private void Update()
    {
        ChangeColor();
    }

    // Performs action when triggerEntered
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.ToLower() == "player")
        {
            Destroy(gameObject);
        }
    }
    

    public void ChangeColor()
    {
        colorTimer += Time.deltaTime;
        if (colorTimer > changeColorTime)
        {
            transform.GetComponent<MeshRenderer>().material.color = enemyColor[Random.Range(0, enemyColor.Length)];
            colorTimer = 0;
        }
    }



}
