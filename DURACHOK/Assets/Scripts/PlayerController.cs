using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float damping = 1.5f;
    [SerializeField] private CharacterController controller;

    private Vector3 inputVector;
    private Vector3 movementVector;
    private Transform cameraTransform; // ������ �� ������
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform; // �������� �������� ������
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetInput();
        RotatePlayer();
        MovePlayer();
    }

    private void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();

        if (inputVector.magnitude >= 0.1f)
        {
            // ����������� ���� � ��������� ���������� ������
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            // ���������� ������������ ������������ (y) ��� ���������� �� ���������
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();

            // ������������ ����������� �������� �� ������ ������
            movementVector = (forward * inputVector.z + right * inputVector.x) * playerSpeed;
        }
        else
        {
            movementVector = Vector3.Lerp(movementVector, Vector3.zero, Time.deltaTime * damping);
        }
    }

    private void RotatePlayer()
    {
        if (inputVector.magnitude >= 0.1f)
        {
            // ������������ ���� �������� �� ������ ����������� ��������
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }

    private void MovePlayer()
    {
        // ���������� movementVector, ��� ������������� ������������ ������
        controller.Move(movementVector * Time.deltaTime);
    }
}
