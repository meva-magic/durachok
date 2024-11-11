using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    public Transform player;
    public float followDistance = 2.0f;
    public float heightOffset = 1.0f;
    public float smoothSpeed = 5.0f;
    public float attachRadius = 2.0f;
    public float rightOffset = 1.5f;

    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isAttached = false;
    private bool wasSpacePressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }

    void Update()
    {
        if (DurachokAbsorption.instance != null && !DurachokAbsorption.instance.canHide)
        {
            // Если Дурачок скрыт в игроке, блокируем привязку
            isAttached = false;
            return;
        }

        // Проверка нажатия пробела и активация привязки
        if (Input.GetKey(KeyCode.Space))
        {
            if (!wasSpacePressed && Vector3.Distance(transform.position, player.position) <= attachRadius)
            {
                isAttached = true;
            }
            wasSpacePressed = true;
        }
        else
        {
            isAttached = false;
            wasSpacePressed = false;
        }

        // Движение к целевой позиции при активной привязке
        if (isAttached)
        {
            Vector3 forwardDirection = player.forward;
            Vector3 rightDirection = player.right;
            targetPosition = player.position + forwardDirection * followDistance + rightDirection * rightOffset;
            targetPosition.y = player.position.y + heightOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }
}
