using UnityEngine;

public class IsometricCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothTime = 0.3f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 initialCameraPosition;

    private void Start()
    {
        initialCameraPosition = transform.position - player.position;
    }

    private void LateUpdate()
    {
        if (player == null) return;
        Vector3 targetPosition = player.position + initialCameraPosition;

        if ((targetPosition - transform.position).sqrMagnitude > 0.001f)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        }
    }
}
