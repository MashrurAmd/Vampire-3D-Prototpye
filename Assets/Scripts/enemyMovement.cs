using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float moveSpeed = 3f;       // Movement speed
    public float rotationSpeed = 5f;   // Rotation speed
    public float detectionRange = 15f; // Distance at which enemy detects the player

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
        if (player == null) return;

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
        }
    }
}


