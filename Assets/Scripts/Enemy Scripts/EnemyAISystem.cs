using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAISystem : MonoBehaviour
{
    public float moveMagnitude = 0.05f;
    public float movementSpeed = 0.5f;
    private float speedMoveMultiplier = 1f;

    public float distanceAttack = 4.5f;
    public float distanceMoveTo = 13f;
    public float turnSpeed = 10f;
    public float patrolRange = 10f;

    private int aiTime = 0;
    private int aiState = 0;

    private Transform playerTarget;
    private Vector3 movementPosition;

    private MovementMotor motor;

    private Animator animator;
    private string PARAMETER_RUN = "Run";
    private string PARAMETER_ATTACK_ONE = "Attack1";
    private string PARAMETER_ATTACK_TWO = "Attack2";

    void Awake()
    {
        
    }

    void Update()
    {
        
    }
}