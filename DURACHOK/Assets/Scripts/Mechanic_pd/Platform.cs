using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pointA; // ��������� �����
    public Transform pointB; // �������� �����
    public float speed = 2f; // �������� �������� ���������
    public float activationDistance = 5f; // ����������, �� ������� ��������� ������ ���������
    private bool isMoving = false; // ������ �������� ���������
    private Transform player; // ������ �� ������

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // ������� ������ �� ����
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= activationDistance)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if (isMoving)
        {
            MovePlatform();
        }
    }

    private void SoundPlay()
    {
        if (isMoving)
        {
            AudioManager.instance.Play("PlatformMove");
        }
        else if (!isMoving)
        {
            AudioManager.instance.Stop("PlatformMove");
        }
    }

    void MovePlatform()
    {
        // ������� ��������� ����� �������
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

        // ����� ��������� ��������� ����� B, ��� ����� ��������� � ����� A
        if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
        {
            Transform temp = pointA;
            pointA = pointB;
            pointB = temp;
        }
    }
}
