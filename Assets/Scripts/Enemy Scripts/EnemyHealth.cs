using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damageAmount)
    {
        print("Enemy damaged");
        health -= damageAmount;

        if(health <= 0)
        {

        }
    }
}