using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private MovementMotor motor;

    public float moveMagnitude = 0.05f;
    public float speed = 0.7f;
    public float speedMoveWhileAttack = 0.1f;
    public float speedAttack = 1.5f;
    public float turnSpeed = 10f;
    public float jumpPower = 20f;

    private float speedMoveMultiplier = 1f;

    private Vector3 direction;
    private Animator animator;
    private Camera mainCamera;

    private string PARAMETER_STATE = "State";
    private string PARAMETER_ATTACK_TYPE = "AttackType";
    private string PARAMETER_ATTACK_INDEX = "AttackIndex";

    public AttackAnimation[] attackAnimations;
    public string[] comboAttackList;
    public int comboType;

    private int attackIndex = 0;
    private string[] comboList;
    private int attackStack;
    private float attackStackTimeTemporary;

    private bool isAttacking;

    void Awake()
    {
        motor = GetComponent<MovementMotor>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.applyRootMotion = false;
        mainCamera = Camera.main;
    }

    void Update()
    {
        MovementAndJumping();
    }

    private Vector3 MoveDirection
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value * speedMoveMultiplier;

            if(direction.magnitude > 0.1f)
            {
                var newRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
            }
            direction *= speed * (Vector3.Dot(transform.forward, direction) + 1f) * 5f;
            motor.Move(direction);

            AnimationMove(motor.characterController.velocity.magnitude * 0.1f);
        }
    }

    void Moving(Vector3 direction, float multiplier)
    {
        speedMoveMultiplier = 1 * multiplier;
        MoveDirection = direction;
    }

    void Jump()
    {
        motor.Jump(jumpPower);
    }

    void AnimationMove (float magnitude)
    {
        if(magnitude > moveMagnitude)
        {
            float speedAnimation = magnitude * 2f;

            if(speedAnimation < 1f)
            {
                speedAnimation = 1f;
            }

            if(animator.GetInteger(PARAMETER_STATE) != 2)
            {
                animator.SetInteger(PARAMETER_STATE, 1);
                animator.speed = speedAnimation;
            }
        }
        else if(animator.GetInteger(PARAMETER_STATE) != 2)
        {
            animator.SetInteger(PARAMETER_STATE, 0);

        }
    }

    void MovementAndJumping()
    {
        Vector3 moveInput = Vector3.zero;
        Vector3 forward = Quaternion.AngleAxis(-90, Vector3.up) * mainCamera.transform.right;

        moveInput += forward * Input.GetAxis("Vertical");
        moveInput += mainCamera.transform.right * Input.GetAxis("Horizontal");

        moveInput.Normalize();
        Moving(moveInput.normalized, 1f);

        if(Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    void ResetCombo()
    {
        attackIndex = 0;
        attackStack = 0;
        isAttacking = false;
    }

    void FightAnimation()
    {
        if (comboList != null && attackIndex >= comboList.Length)
        {
            ResetCombo();
        } else if (comboList != null && comboList.Length > 0)
        {
            int motionIndex = int.Parse(comboList[attackIndex]);

            if(motionIndex < attackAnimations.Length)
            {
                animator.SetInteger(PARAMETER_STATE, 2);
                animator.SetInteger(PARAMETER_ATTACK_TYPE, comboType);
                animator.SetInteger(PARAMETER_ATTACK_INDEX, attackIndex);
            }
        }
    }

    void HandleAttackAnimations()
    {
        if(Time.time > attackStackTimeTemporary + 0.5f)
        {
            attackStack = 0;
        }
        comboList = comboAttackList[comboType].Split("," [0]);

        if(animator.GetInteger(PARAMETER_STATE) == 2)
        {
            animator.speed = speedAttack;

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if(stateInfo.IsTag("Attack"))
            {
                int motionIndex = int.Parse(comboList[attackIndex]);

                if(stateInfo.normalizedTime > 0.9f)
                {
                    animator.SetInteger(PARAMETER_STATE, 0);
                    isAttacking = false;
                    attackIndex++;

                    if(attackStack > 1)
                    {
                        FightAnimation();
                    }
                    else if(attackIndex >= comboList.Length)
                    {
                        ResetCombo();
                    }
                }
            }
        }
    }

    void Attack()
    {

    }
}