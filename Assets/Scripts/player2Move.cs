using UnityEngine;

public class player2Move : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 9f;      // ✅ Sprint speed
    public float jumpForce = 5f;


    [Header("Camera Settings")]
    public Transform cameraTransform; // Main Camera
    public float mouseSensitivity = 100f;

    private Rigidbody rb;
    private bool isGrounded;
    private Animator animator;

    private float xRotation = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        // Lock and hide the mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Detect walking
        bool isWalking = (x != 0 || z != 0);
        animator.SetBool("isWalking", isWalking);

        // Sprinting (Shift + forward)
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0;
        animator.SetBool("isRunning", isSprinting);

        // Jump


        // Camera rotation with mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // Prevent looking too far up/down

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void FixedUpdate()
    {
        // Movement with Rigidbody
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Check sprint
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0;
        float currentSpeed = isSprinting ? sprintSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3 velocity = move.normalized * currentSpeed;
        velocity.y = rb.velocity.y;

        rb.velocity = velocity;
    }
}


