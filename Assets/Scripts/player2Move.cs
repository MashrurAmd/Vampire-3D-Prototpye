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

    public SwordDamageControl swordDamage; // drag your sword object here in inspector
    public EnemyHealth enemyHealth; // drag your enemy health script here in inspector



    private float xRotation = 0f;

    public GameObject enemy;
    public float AttackRange = 2f; // Range for attack detection

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Inside player2Move.cs
    

    public void EnableSwordDamage()
    {
        swordDamage.EnableDamage();
        enemyHealth.TakeDamage(5); 
    }

    public void DisableSwordDamage()
    {
        swordDamage.DisableDamage();
    }


    void Update()
    {
        // Movement input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool pressingW = Input.GetKey(KeyCode.W);
        bool pressingA = Input.GetKey(KeyCode.A);
        bool pressingS = Input.GetKey(KeyCode.S);
        bool pressingD = Input.GetKey(KeyCode.D);

        // Walking
        bool isWalking = (x != 0 || z != 0);
        animator.SetBool("isWalking", isWalking);

        // Sprinting
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && z > 0;
        animator.SetBool("isRunning", isSprinting);


        // Clapping animation
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("isClapping");
        }

        //if mouse left click, play attack animation
        if (Input.GetMouseButtonDown(0))
        {
            
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            animator.SetTrigger("isAttack");
        }



        // ✅ Forward-Right and Forward-Left detection
        bool forwardRight = pressingW && pressingD;
        bool forwardLeft = pressingW && pressingA;


        // ✅ Backward-Right and Backward-Left detection

        bool backwardRight = pressingS && pressingD;
        bool backwardLeft = pressingS && pressingA;

        //Set animator for backwardRight and backwardLeft
        animator.SetBool("backwardRight", backwardRight);
        animator.SetBool("backwardLeft", backwardLeft);

        // Set animator for forwardRight and forwardLeft
        animator.SetBool("forwardRight", forwardRight);
        animator.SetBool("forwardLeft", forwardLeft);

        // ✅ Running Forward-Right & Forward-Left (only if sprinting)
        bool runningForwardRight = isSprinting && forwardRight;
        bool runningForwardLeft = isSprinting && forwardLeft;

        animator.SetBool("runningForwardRight", runningForwardRight);
        animator.SetBool("runningForwardLeft", runningForwardLeft);




        if (runningForwardRight || runningForwardLeft)
        {
            animator.SetBool("forwardRight", false);
            animator.SetBool("forwardLeft", false);
        }

        // ✅ Single-direction walking
        bool walkRight = pressingD && !pressingW && !pressingS && !pressingA;
        bool walkLeft = pressingA && !pressingW && !pressingS && !pressingD;
        bool walkForward = pressingW && !pressingA && !pressingD && !pressingS;
        bool walkBackward = pressingS && !pressingA && !pressingD && !pressingW;

        animator.SetBool("walkRight", walkRight);
        animator.SetBool("walkLeft", walkLeft);

        animator.SetBool("walkBackward", walkBackward);

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


