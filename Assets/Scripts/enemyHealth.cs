using UnityEngine;
using TMPro;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Animator animator;
    public TextMeshProUGUI healthText; // Optional: UI text to display health

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Update health UI if assigned
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{gameObject.name} took {amount} damage. HP left: {currentHealth}");

        animator.SetTrigger("Hit"); // Optional: hit reaction animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        GetComponent<Collider>().enabled = false; // Disable enemy hitbox
        this.enabled = false; // Stop this script
        // Optionally stop movement AI
    }
}
