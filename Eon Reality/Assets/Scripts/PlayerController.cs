using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Exposed Variables
    [SerializeField]
    private SceneController sceneController;

    [SerializeField]
    private float moveSpeed = 10.0f;

    [SerializeField]
    private float rotationSpeed = 10.0f;

    #endregion

    #region Non-Exposed Variables

    Rigidbody playerRB;

    private float zMove;
    
    private float yRotate;
    
    Vector3 moveDir = Vector3.zero;
    
    Vector3 moveVelocity = Vector3.zero;

    Vector3 originalPlayerPosition = Vector3.zero;

    #endregion

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

        groundPlayer();

    }

    // Performs action when triggerEntered
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.ToLower() == "enemy")
        {
            sceneController.UpdateScore();
            sceneController.onEnemyDied(other.transform);
            transform.GetComponent<MeshRenderer>().material.color = other.transform.GetComponent<MeshRenderer>().material.color;

        }

    }

    // Keeps the player grounded
    void groundPlayer()
    {
        if(transform.localEulerAngles.x != 0 || transform.localEulerAngles.z != 0)
        {
            transform.localEulerAngles = new Vector3(0.0f,transform.localEulerAngles.y,0.0f);
        }
    }
}
