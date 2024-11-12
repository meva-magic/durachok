using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private CharacterController controller;

    private Vector3 movementVector;
    private Vector3 velocity;
    private bool isGrounded;
    private Transform cameraTransform;

    public static PlayerController instance;
    public bool isMovementAllowed = true;  // ƒобавл€ем переменную дл€ контрол€ движений

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!isMovementAllowed) return; // ѕрерывание работы, если движени€ не разрешены
        GetInputAndMovement();
        ApplyGravity();
        MovePlayer();
    }

    private void GetInputAndMovement()
    {
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputVector.sqrMagnitude >= 0.01f)
        {
            inputVector.Normalize();

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;

            movementVector = (forward * inputVector.z + right * inputVector.x) * playerSpeed;

            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            movementVector = Vector3.Lerp(movementVector, Vector3.zero, Time.deltaTime * damping);
        }
    }

    private void ApplyGravity()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
    }

    private void MovePlayer()
    {
        Vector3 finalMovement = movementVector + velocity;
        controller.Move(finalMovement * Time.deltaTime);
    }
}
