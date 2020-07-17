using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Color[] enemyColor = {Color.black,Color.blue,Color.yellow,Color.red,Color.green,Color.grey,Color.magenta};
    
    void Start()
    {
        transform.GetComponent<MeshRenderer>().material.color = enemyColor[Random.Range(0,enemyColor.Length)];      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.ToLower() == "player")
        {
            Destroy(gameObject);
        }
    }

    
    
}
