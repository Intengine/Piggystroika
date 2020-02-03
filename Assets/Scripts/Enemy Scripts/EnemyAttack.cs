using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public LayerMask playerLayer;
    public float damage = 1f;
    public float radius = 0.3f;

    private PlayerHealth playerHealth;
    private bool collided;

    void Update()
    {
        CheckForDamage();
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, playerLayer);

        foreach (Collider h in hits)
        {
            playerHealth = h.GetComponent<PlayerHealth>();

            if (playerHealth)
            {
                collided = true;
            }
        }
        if (collided)
        {
            collided = false;
            playerHealth.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}