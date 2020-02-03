using UnityEngine;

public class MovementMotor : MonoBehaviour
{
    public float gravityMultiplier = 1f;
    public float lerpTime = 10f;

    [HideInInspector]
    public CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 targetDirection = Vector3.zero;
    private float fallVelocity = 0f;

    public float distanceToGround = 0.1f;

    private bool isGrounded;
    private Collider myCollider;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        myCollider = GetComponent<Collider>();
    }

    void Start()
    {
        distanceToGround = myCollider.bounds.extents.y;
    }

    void Update()
    {
        isGrounded = OnGroundCheck();
        moveDirection = Vector3.Lerp(moveDirection, targetDirection, Time.deltaTime * lerpTime);
        moveDirection.y = fallVelocity;

        characterController.Move(moveDirection * Time.deltaTime);

        if(!isGrounded)
        {
            fallVelocity -= 90f * gravityMultiplier * Time.deltaTime;
        }
    }

    public bool OnGroundCheck()
    {
        RaycastHit hit;

        if(characterController.isGrounded)
        {
            return true;
        }

        if(Physics.Raycast(myCollider.bounds.center, -Vector3.up, out hit, distanceToGround + 0.1f))
        {
            return true;
        }
        return false;
    }

    public void Move(Vector3 direction)
    {
        targetDirection = direction;
    }

    public void Stop()
    {
        moveDirection = Vector3.zero;
        targetDirection = Vector3.zero;
    }

    public void Jump(float jumpPower)
    {
        if(isGrounded)
        {
            fallVelocity = jumpPower;
        }
    }
}