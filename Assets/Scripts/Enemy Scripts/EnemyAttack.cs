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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}