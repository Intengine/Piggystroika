using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXDamage : MonoBehaviour
{
    public LayerMask enemyLayer;
    public float damage = 20f;
    public float radius = 2f;

    private EnemyHealth enemyHealth;
    private bool collided;

    void Update()
    {
        CheckForDamage();
    }

    void CheckForDamage()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius, enemyLayer);

        foreach(Collider h in hits)
        {
            enemyHealth = h.GetComponent<EnemyHealth>();

            if(enemyHealth)
            {
                collided = true;
            }
        }
        if(collided)
        {
            collided = false;
            enemyHealth.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}