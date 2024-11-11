using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private float gravity = -9.81f;  // Сила гравитации
    [SerializeField] private float jumpHeight = 2f;   // Высота прыжка
    [SerializeField] private CharacterController controller;

    private Vector3 movementVector;
    private Vector3 velocity;  // Вертикальная скорость
    private bool isGrounded;
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
        ApplyGravity();
        MovePlayer();
    }

    private void GetInputAndMovement()
    {
        // Считываем оси для движения, но используем условие для уменьшения вычислений
        Vector3 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputVector.sqrMagnitude >= 0.01f) // Сравнение через квадрат модуля быстрее
        {
            inputVector.Normalize();

            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;
            forward.y = 0;
            right.y = 0;

            // Вычисляем движение относительно камеры
            movementVector = (forward * inputVector.z + right * inputVector.x) * playerSpeed;

            // Поворот
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
        isGrounded = controller.isGrounded;  // Проверяем, на земле ли персонаж

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Когда персонаж на земле, сбрасываем вертикальную скорость
        }

        // Применяем гравитацию
        if (!isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;  // Падение
        }
    }

    private void MovePlayer()
    {
        // Двигаем персонажа, учитывая горизонтальное и вертикальное движение
        Vector3 finalMovement = movementVector + velocity;
        controller.Move(finalMovement * Time.deltaTime);
    }
}
