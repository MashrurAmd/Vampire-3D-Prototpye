using UnityEngine;


public class PlayerMovement3D : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;


    void Start()
    {
        controller = player.GetComponent<CharacterController>();
    }

    void Update()
    {


        // Get input
        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");   

        // Move relative to the player's forward direction
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jumping


        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
