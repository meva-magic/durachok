using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    public Transform player;        // Ссылка на игрока
    public Vector3 offset = new Vector3(10, 10, -10); // Смещение камеры
    public float smoothSpeed = 0.125f;  // Скорость сглаживания движения камеры

    void LateUpdate()
    {
        // Рассчитываем желаемую позицию камеры
        Vector3 desiredPosition = player.position + offset;

        // Плавно перемещаем камеру в сторону желаемой позиции
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Устанавливаем новую позицию камеры
        transform.position = smoothedPosition;

        // Камера всегда смотрит на игрока
        transform.LookAt(player);
    }
}
