using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform pointA; // Начальная точка
    public Transform pointB; // Конечная точка
    public float speed = 2f; // Скорость движения платформы
    public float activationDistance = 5f; // Расстояние, на котором платформа начнёт двигаться
    private bool isMoving = false; // Статус движения платформы
    private Transform player; // Ссылка на игрока

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Находим игрока по тегу
    }

    void Update()
    {
        // Если игрок близко, платформа начинает двигаться
        if (Vector3.Distance(transform.position, player.position) <= activationDistance)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false; // Если игрок слишком далеко, платформа перестает двигаться
        }

        if (isMoving)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        // Двигаем платформу между точками
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pointB.position, step);

        // Когда платформа достигнет точки B, она будет двигаться в точку A
        if (Vector3.Distance(transform.position, pointB.position) < 0.1f)
        {
            Transform temp = pointA;
            pointA = pointB;
            pointB = temp;
        }
    }
}
