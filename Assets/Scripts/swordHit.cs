using UnityEngine;

public class SwordHit : MonoBehaviour
{
    public int damage = 20; // Damage per hit
    private bool canDamage = false;

    // Called from animation event
    public void EnableDamage()
    {
        canDamage = true;
    }

    // Called from animation event
    public void DisableDamage()
    {
        canDamage = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canDamage) return; // Don't damage unless attacking

        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
