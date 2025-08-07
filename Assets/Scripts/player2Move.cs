using UnityEngine;

public class player2Move : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 9f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;

    [Header("Camera Settings")]
    public float mouseSensitivity = 100f;

    private Rigidbody rb;
    private Animator animator;

    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Walking
        bool isWalking = (x != 0 || z != 0);
        animator.SetBool("isWalking", isWalking);

        // Sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0;
        animator.SetBool("isRunning", isSprinting);

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("isJumping");
        }

        // Clapping animation
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("isClapping");
        }

        // ✅ Forward-Right and Forward-Left detection
        bool forwardRight = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D);
        bool forwardLeft = Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A);

        animator.SetBool("forwardRight", forwardRight);
        animator.SetBool("forwardLeft", forwardLeft);

        // Reset if neither
        if (!forwardRight && !forwardLeft)
        {
            animator.SetBool("forwardRight", false);
            animator.SetBool("forwardLeft", false);
        }

        // Mouse camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 velocity = move.normalized * currentSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }
}


