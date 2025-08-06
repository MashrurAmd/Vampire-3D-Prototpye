using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f; // Height of jump

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;

    private bool isWalking;

    void Start()
    {
        controller = player.GetComponent<CharacterController>();
        animator = player.GetComponentInChildren<Animator>(); 
    }

    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        
        isWalking = z != 0;
        animator.SetBool("isWalking", isWalking);

        
        Vector3 move = player.transform.right * x + player.transform.forward * z;
        controller.Move(move * moveSpeed * Time.deltaTime);

        
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Apply jump velocity
            animator.SetTrigger("isJumping"); 
        }

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Keep grounded
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}






