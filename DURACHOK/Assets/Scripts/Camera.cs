using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(10, 10, -10); 
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (player == null) return; // Проверка, что игрок установлен

        Vector3 targetPosition = player.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
