using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    public Transform player; // Ссылка на объект игрока
    public float followDistance = 2.0f; // Расстояние от игрока, где Durachok будет находиться
    public float heightOffset = 1.0f; // Высотный сдвиг относительно игрока
    public float smoothSpeed = 5.0f; // Плавность движения

    public float attachRadius = 2.0f; // Радиус, на котором можно активировать привязку
    public float rightOffset = 1.5f; // Дополнительный сдвиг вправо от центра игрока

    private Rigidbody rb; // Rigidbody для плавности движения
    private Vector3 targetPosition; // Целевая позиция для движения
    private bool isAttached = false; // Флаг привязки
    private bool wasSpacePressed = false; // Флаг для отслеживания состояния пробела

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Включаем кинематику, чтобы контролировать движение через transform
    }

    void Update()
    {
        // Проверка, если пробел удерживается
        if (Input.GetKey(KeyCode.Space))
        {
            // Если пробел был только что нажат и Durachok в пределах радиуса активации
            if (!wasSpacePressed && Vector3.Distance(transform.position, player.position) <= attachRadius)
            {
                isAttached = true; // Устанавливаем привязку активной
            }

            // Обновляем флаг, что пробел удерживается
            wasSpacePressed = true;
        }
        else
        {
            // Если пробел отпущен, разрываем привязку
            isAttached = false;
            wasSpacePressed = false;
        }

        // Если привязка активирована, вычисляем целевую позицию
        if (isAttached)
        {
            // Вычисление позиции перед игроком и чуть правее его центра
            Vector3 forwardDirection = player.forward;
            Vector3 rightDirection = player.right;

            // Целевая позиция с высотой, смещенная от центра игрока вправо
            targetPosition = player.position + forwardDirection * followDistance + rightDirection * rightOffset;
            targetPosition.y = player.position.y + heightOffset; // Установить высоту в зависимости от игрока

            // Плавное движение Durachok к целевой позиции через transform.position
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
        }
    }
}
