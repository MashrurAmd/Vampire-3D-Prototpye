using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    //private Animator animator;

    private bool isRunning;

    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        //animator = player.GetComponent<Animator>(); // Make sure Animator is on the player
    }

    void Update()
    {
        // Get input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Determine running
        isRunning = Input.GetKey(KeyCode.W) && z > 0;
        //animator.SetBool("isRunning", isRunning);

        // Move relative to the player's forward direction
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
