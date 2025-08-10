using UnityEngine;

public class SwordDamageControl : MonoBehaviour
{
    private Collider swordCollider;

    void Start()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false; // disable by default
    }

    public void EnableDamage()
    {
        swordCollider.enabled = true;
    }

    public void DisableDamage()
    {
        swordCollider.enabled = false;
    }
}
