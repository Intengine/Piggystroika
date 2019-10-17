using System.Collections;
using System.Collections.Generic;
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

            if (direction.magnitude > 0.1f)
            {
                var newRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * turnSpeed);
            }
            direction *= speed * (Vector3.Dot(transform.forward, direction) + 1f) * 5f;
            motor.Move(direction);

            // AnimationMove(motor.characterController.velocity.magnitude * 0.1f);
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

    void MovementAndJumping()
    {
        Vector3 moveInput = Vector3.zero;
        Vector3 forward = Quaternion.AngleAxis(-90, Vector3.up) * mainCamera.transform.right;

        moveInput += forward * Input.GetAxis("Vertical");
        moveInput += mainCamera.transform.right * Input.GetAxis("Horizontal");

        moveInput.Normalize();
        Moving(moveInput.normalized, 1f);

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
}