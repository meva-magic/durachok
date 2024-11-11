using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;  // �����, ����� �������� �������� ���������
    public float maxSpeed = 2f;  // ������������ �������� ���������
    public float minSpeed = 0.5f;  // ����������� �������� ��������� ��� ����������� � �����
    public float slowdownDistance = 1f;  // ���������, �� ������� �������� ����������

    private int targetPointIndex = 0;
    private bool isMoving = false;

    private void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void ToggleMovement()
    {
        isMoving = !isMoving;

        if (isMoving)
        {
            AudioManager.instance.Play("PlatformMove");
        }
        else
        {
            AudioManager.instance.Play("PlatformStop");
        }
    }

    private void MoveTowardsTarget()
    {
        if (points.Length < 2) return;

        Transform targetPoint = points[targetPointIndex];
        float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);

        // ������������ ������� �������� �� ������ ���������� �� ����
        float speed = Mathf.Lerp(minSpeed, maxSpeed, distanceToTarget / slowdownDistance);
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (distanceToTarget < 0.1f)
        {
            // ������� � ��������� �����
            targetPointIndex = (targetPointIndex + 1) % points.Length;
        }
    }
}
