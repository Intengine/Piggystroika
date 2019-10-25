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
        animator = GetComponent<Animator>();
        motor = GetComponent<MovementMotor>();
    }

    void Update()
    {
        EnemyAI();
    }

    void EnemyAI()
    {
        float distance = Vector3.Distance(movementPosition, transform.position);
        Quaternion targetRotation = Quaternion.LookRotation(movementPosition - transform.position);
        targetRotation.x = 0f;
        targetRotation.z = 0f;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);

        if(playerTarget != null)
        {
            movementPosition = playerTarget.position;

            if(aiTime <= 0)
            {
                aiState = Random.Range(0, 4);
                aiTime = Random.Range(10, 100);
            }
            else
            {
                aiTime--;
            }

            if(distance <= distanceAttack)
            {
                if(aiState == 0)
                {
                    // Attack();
                }
            } else if(distance <= distanceMoveTo)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            }
            else
            {
                playerTarget = null;

                if(aiState == 0)
                {
                    aiState = 1;
                    aiTime = Random.Range(10, 500);

                    movementPosition = transform.position + new Vector3(Random.Range(-patrolRange, patrolRange), 0f, Random.Range(-patrolRange, patrolRange));
                }
            }
        }
        else
        {
            GameObject target = GameObject.FindGameObjectWithTag("Player");
            float targetDistance = Vector3.Distance(target.transform.position, transform.position);

            if(targetDistance <= distanceMoveTo || targetDistance <= distanceAttack)
            {
                playerTarget = target.transform;
            } else if(aiState == 0)
            {
                aiState = 1;
                aiTime = Random.Range(10, 200);

                movementPosition = transform.position + new Vector3(Random.Range(-patrolRange, patrolRange), 0f, Random.Range(-patrolRange, patrolRange));
            } else if(aiTime <= 0)
            {
                aiState = Random.Range(0, 4);
                aiTime = Random.Range(10, 200);
            }
            else
            {
                aiTime--;
            }
        }
    }
}