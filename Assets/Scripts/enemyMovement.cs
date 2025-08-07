using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    
    public float rotationSpeed = 5f;   // Rotation speed
    public float CLoseDetectionRange = 10f; 
    public float farDetectionRange = 20f; 
    public Animator animator; // Reference to the Animator component


    private Transform player;

    void Start()
    {
        // Automatically find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {

        ChasePlayer();
        /*if (player == null) return;

        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.position);

        // Only move if the player is within detection range
        if (distance <= detectionRange)
        {



            // Move towards player in in y+1 direction 
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Keep movement on the horizontal plane
            transform.position += direction * moveSpeed * Time.deltaTime;



  
            direction.y = 0; // Keep rotation on the horizontal plane
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            }
        }*/
    }


    //create a method to detect if the player is within close or far detection range and perform diffrent bools in animator also rottion towards player in one single function using if else
    private void ChasePlayer()
    {
        if (player == null) return;
        // Calculate distance to player
        float distance = Vector3.Distance(transform.position, player.position);
        // Check if the player is within close detection range
        if (distance <= CLoseDetectionRange)
        {
            // Perform close detection logic (e.g., set animator bools)
            Debug.Log("Player is within close detection range.");
            // Set animator bools for close detection here
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);

        }
        else if (distance <= farDetectionRange)
        {
            // Perform far detection logic (e.g., set animator bools)
            Debug.Log("Player is within far detection range.");
            // Set animator bools for far detection here
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            // Player is out of detection range, do nothing or reset animator bools
            Debug.Log("Player is out of detection range.");
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
            return;
        }
        // Rotate towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0; // Keep rotation on the horizontal plane
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

}


