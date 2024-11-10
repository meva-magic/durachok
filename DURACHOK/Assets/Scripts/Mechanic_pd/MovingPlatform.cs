using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;  // Точки, между которыми движется платформа
    public float maxSpeed = 2f;  // Максимальная скорость платформы
    public float minSpeed = 0.5f;  // Минимальная скорость платформы при приближении к точке
    public float slowdownDistance = 1f;  // Дистанция, на которой начнется замедление

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
        // Переключаем состояние движения
        isMoving = !isMoving;
    }

    private void MoveTowardsTarget()
    {
        if (points.Length < 2) return;

        Transform targetPoint = points[targetPointIndex];
        float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);

        // Рассчитываем текущую скорость на основе расстояния до цели
        float speed = Mathf.Lerp(minSpeed, maxSpeed, distanceToTarget / slowdownDistance);
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed);

        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (distanceToTarget < 0.1f)
        {
            // Переход к следующей точке
            targetPointIndex = (targetPointIndex + 1) % points.Length;
        }
    }
}
