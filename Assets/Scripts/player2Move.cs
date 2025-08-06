using UnityEngine;

public class player2Move : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public Transform groundCheck;       // Empty GameObject at feet
    public float groundDistance = 0.2f; // Radius for ground detection
    public LayerMask groundMask;        // LayerMask for ground

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); // ✅ Animator on child model
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Walking animation
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        bool isWalking = (x != 0 || z != 0); // ✅ Moving in any direction
        animator.SetBool("isWalking", isWalking);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping"); // ✅ Trigger jump animation
        }
    }

    void FixedUpdate()
    {
        // Movement with Rigidbody
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0, z).normalized * moveSpeed;
        Vector3 velocity = new Vector3(move.x, rb.velocity.y, move.z);
        rb.velocity = velocity;
    }
}
