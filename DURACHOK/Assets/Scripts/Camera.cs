using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [SerializeField] private Transform player;           // Ссылка на игрока
    [SerializeField] private Vector3 offset = new Vector3(10, 10, -10); // Смещение камеры
    [SerializeField] private float smoothTime = 0.3f; // Время сглаживания движения камеры

    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (player == null) return; // Проверка, что игрок установлен

        // Рассчитываем целевую позицию с учетом смещения
        Vector3 targetPosition = player.position + offset;

        // Плавно перемещаем камеру с помощью SmoothDamp
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

        // Направляем камеру на игрока
        transform.LookAt(player.position + Vector3.up * 1.5f); // Чуть выше игрока для лучшего обзора
    }
}
