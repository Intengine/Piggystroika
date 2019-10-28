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
                    Attack();
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
        MoveToPosition(movementPosition, 1f, motor.characterController.velocity.magnitude);
    }

    void MoveToPosition(Vector3 position, float speedMultiplier, float magnitude)
    {
        float speed = movementSpeed * speedMoveMultiplier * 2 * 5 * speedMultiplier;
        Vector3 direction = position - transform.position;
        Quaternion newRotation = transform.rotation;

        direction.y = 0f;

        if(direction.magnitude > 0.1f)
        {
            motor.Move(direction.normalized * speed);
            newRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            motor.Stop();
        }
        AnimationMove(magnitude * 0.1f);
        CheckIfAttackEnded();
    }

    void AnimationMove(float magnitude)
    {
        if(magnitude > moveMagnitude)
        {
            float speedAnimation = magnitude * 2f;

            if(speedAnimation < 1)
            {
                speedAnimation = 1f;
            } else if(!animator.GetBool(PARAMETER_RUN))
            {
                animator.SetBool(PARAMETER_RUN, true);
                animator.speed = speedAnimation;
            }
        }
        else if(animator.GetBool(PARAMETER_RUN))
        {
            animator.SetBool(PARAMETER_RUN, false);
        }
    }

    void Attack()
    {
        if(Random.Range(0, 2) > 0)
        {
            animator.SetBool(PARAMETER_ATTACK_ONE, true);
        }
        else
        {
            animator.SetBool(PARAMETER_ATTACK_TWO, true);
        }
    }

    void CheckIfAttackEnded()
    {
        if(animator.GetCurrentAnimatorStateInfo(0).IsName(PARAMETER_ATTACK_ONE))
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                animator.SetBool(PARAMETER_ATTACK_ONE, false);
                animator.SetBool(PARAMETER_RUN, false);
            }
        } else if(animator.GetCurrentAnimatorStateInfo(0).IsName(PARAMETER_ATTACK_TWO))
        {
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                animator.SetBool(PARAMETER_ATTACK_TWO, false);
                animator.SetBool(PARAMETER_RUN, false);
            }
        }
    }
}