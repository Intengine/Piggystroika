using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;

    public void TakeDamage(float damageAmount)
    {
        print("P1 damaged");
        health -= damageAmount;

        if(health <= 0)
        {

        }
    }
}