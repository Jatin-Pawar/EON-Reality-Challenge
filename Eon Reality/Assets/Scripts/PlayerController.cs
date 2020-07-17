using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float rotationSpeed = 10.0f;

    [SerializeField]
    private SceneController sceneController;

    Rigidbody playerRB;

    private float zMove;
    
    private float yRotate;
    
    Vector3 moveDir = Vector3.zero;
    
    Vector3 moveVelocity = Vector3.zero;

    Vector3 originalPlayerPosition = Vector3.zero;
    
    void Start()
    {
        playerRB = transform.GetComponent<Rigidbody>();
        originalPlayerPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        //Reset enemy when it falls down from the platform
        if(transform.localPosition.y < 0.0f)
        {
            transform.localPosition = originalPlayerPosition;
            transform.localRotation = Quaternion.identity;
            playerRB.velocity = Vector3.zero;
        }

        //Get input from user for Movement and Rotation
        yRotate = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        //Changing rotation as per user input
        transform.localEulerAngles += new Vector3(0,yRotate*rotationSpeed,0);

        moveDir = new Vector3(0, 0, zMove);
        moveVelocity = moveDir * moveSpeed;

        //Adding form to move
        playerRB.AddRelativeForce(moveVelocity);


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.ToLower() == "enemy")
        {
            sceneController.UpdateScore();
            sceneController.UpdateEnemyCount();
            transform.GetComponent<MeshRenderer>().material.color = collision.transform.GetComponent<MeshRenderer>().material.color;

        }
    }
}
