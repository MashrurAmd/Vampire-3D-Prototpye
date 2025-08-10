using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float rotationSpeed = 5f;          // Rotation speed
    public float closeDetectionRange = 10f;   // Close range (run)
    public float farDetectionRange = 20f;     // Far range (walk)
    public float attackRange = 2f;            // Attack range
    public Animator animator;                 // Animator reference

    private Transform player;

    void Start()
    {
        // Find player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange)
        {
            // Attack mode
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            Debug.Log("Attacking player!");
            animator.SetBool("isAttack", true);
            player.GetComponent<PlayerHealth>()?.TakeDamage(10);
            


        }
        else if (distance <= closeDetectionRange)
        {
            // Run mode
            Debug.Log("Player is within close detection range.");
            animator.SetBool("isAttack", false);
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else if (distance <= farDetectionRange)
        {
            // Walk mode
            Debug.Log("Player is within far detection range.");
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            // Idle mode
            Debug.Log("Player is out of detection range.");
            animator.SetBool("isAttack", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            return;
        }

        // Rotate towards player (unless attacking and you want a stationary attack)
        if (distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0;
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }


    }
}



