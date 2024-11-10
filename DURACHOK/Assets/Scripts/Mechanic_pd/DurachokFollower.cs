using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    public Transform player; // ������ �� ������ ������
    public float followDistance = 2.0f; // ���������� �� ������, ��� Durachok ����� ����������
    public float heightOffset = 1.0f; // �������� ����� ������������ ������
    public float smoothSpeed = 5.0f; // ��������� ��������

    public float attachRadius = 2.0f; // ������, �� ������� ����� ������������ ��������

    private Rigidbody rb; // Rigidbody ��� ��������� ��������
    private Vector3 targetPosition; // ������� ������� ��� ��������
    private bool isAttached = false; // ���� ��������
    private bool wasSpacePressed = false; // ���� ��� ������������ ��������� �������

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // ��������, ���� ������ ������������
        if (Input.GetKey(KeyCode.Space))
        {
            // ���� ������ ��� ������ ��� ����� � Durachok � �������� ������� ���������
            if (!wasSpacePressed && Vector3.Distance(transform.position, player.position) <= attachRadius)
            {
                isAttached = true; // ������������� �������� ��������
            }

            // ��������� ����, ��� ������ ������������
            wasSpacePressed = true;
        }
        else
        {
            // ���� ������ �������, ��������� ��������
            isAttached = false;
            wasSpacePressed = false;
        }

        // ���� �������� ������������, ��������� ������� �������
        if (isAttached)
        {
            // ���������� ������� ����� � ���� ������ ������
            Vector3 forwardDirection = player.forward;
            Vector3 rightDirection = player.right;

            // ������� ������� � �������, ��������� �� ����������� ������
            targetPosition = player.position + forwardDirection * followDistance + rightDirection * followDistance / 2;
            targetPosition.y = player.position.y + heightOffset; // ���������� ������ � ����������� �� ������

            // ������� �������� Durachok � ������� �������
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

            // ���������� �������� �������� � Rigidbody
            rb.MovePosition(smoothPosition);
        }
    }
}
