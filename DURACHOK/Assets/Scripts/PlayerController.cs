using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private CharacterController controller;

    private Vector3 movementVector;
    private Transform cameraTransform;
    public static PlayerController instance;

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
        GetInputAndMovement();
        MovePlayer();
    }

    private void GetInputAndMovement()
    {
        // —читываем оси дл€ движени€, но используем условие дл€ уменьшени€ вычислений
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputVector.sqrMagnitude >= 0.01f) // —равнение через квадрат модул€ быстрее
        {
            inputVector.Normalize();

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;

            // ¬ычисл€ем движение относительно камеры
            movementVector = (forward * inputVector.z + right * inputVector.x) * playerSpeed;

            // ѕоворот
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
        else
        {
            movementVector = Vector3.Lerp(movementVector, Vector3.zero, Time.deltaTime * damping);
        }
    }

    private void MovePlayer()
    {
        controller.Move(movementVector * Time.deltaTime);
    }
}
