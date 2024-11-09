using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;  // �����, ����� �������� �������� ���������
    public float speed = 2f;
    private int targetPointIndex = 0;
    private bool isMoving = false;
    private bool isPaused = false;  // ����� ���� ��� ����� ���������

    private void Update()
    {
        if (isMoving && !isPaused)
        {
            MoveTowardsTarget();
        }
    }

    public void ToggleMovement()
    {
        if (isMoving)
        {
            isPaused = true;  // ���������������� ���������
        }
        else
        {
            isPaused = false;  // ������������ �������� ���������
            isMoving = true;   // �������� ��������, ���� ��� �� ��������
        }
    }

    private void MoveTowardsTarget()
    {
        if (points.Length < 2) return;  // ���� ����� ������ ����, �� ����������

        Transform targetPoint = points[targetPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // ������� � ��������� �����
            targetPointIndex = (targetPointIndex + 1) % points.Length;
        }
    }
}
