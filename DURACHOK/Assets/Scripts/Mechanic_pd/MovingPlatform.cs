using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;  // Точки, между которыми движется платформа
    public float speed = 2f;
    private int targetPointIndex = 0;
    private bool isMoving = false;
    private bool isPaused = false;  // Новый флаг для паузы платформы

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
            isPaused = true;  // Приостанавливаем платформу
        }
        else
        {
            isPaused = false;  // Возобновляем движение платформы
            isMoving = true;   // Начинаем движение, если еще не началось
        }
    }

    private void MoveTowardsTarget()
    {
        if (points.Length < 2) return;  // Если точек меньше двух, не продолжаем

        Transform targetPoint = points[targetPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            // Переход к следующей точке
            targetPointIndex = (targetPointIndex + 1) % points.Length;
        }
    }
}
