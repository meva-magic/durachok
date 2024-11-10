using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    [SerializeField] private Transform player;               // Ссылка на игрока
    [SerializeField] private float followRadius = 1f;        // Радиус следования
    [SerializeField] private float followSpeed = 2f;         // Скорость следования
    [SerializeField] private float levitateHeight = 1f;      // Высота левитации
    [SerializeField] private float activationRadius = 5f;    // Радиус для способности
    [SerializeField] private float smoothTime = 0.3f;        // Время для сглаживания

    private bool isFollowing = false;        // Следует ли за игроком
    private bool isAbsorbed = false;         // Находится ли внутри игрока

    private DurachokAbsorption absorptionScript;
    private Collider durachokCollider;
    private Vector3 velocity = Vector3.zero; // Переменная для SmoothDamp

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Получаем ссылку на компонент DurachokAbsorption
        absorptionScript = GetComponent<DurachokAbsorption>();
        absorptionScript.player = player;

        // Получаем коллайдер для отключения при привязке
        durachokCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Включаем следование при нажатии на пробел, если в радиусе
        if (Input.GetKeyDown(KeyCode.Space) && distanceToPlayer <= activationRadius)
        {
            isFollowing = true;
            durachokCollider.enabled = false; // Отключаем коллайдер при привязке к игроку
        }

        // Останавливаем следование, когда пробел отпускается
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isFollowing = false;
            durachokCollider.enabled = true; // Включаем коллайдер при остановке следования
        }

        // Запуск способности поглощения при нажатии Enter, если в радиусе
        if (Input.GetKeyDown(KeyCode.Return) && distanceToPlayer <= activationRadius && !isAbsorbed)
        {
            isAbsorbed = true;
            durachokCollider.enabled = false; // Отключаем коллайдер при поглощении
            StartCoroutine(absorptionScript.AbsorbIntoPlayer());
            Invoke(nameof(ResetAbsorbedStatus), absorptionScript.invisibleDuration + 1f);
        }

        // Следование за игроком, если активировано и не поглощен
        if (isFollowing && !isAbsorbed && !absorptionScript.isInvisible)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        // Рассчитываем точку на заданном радиусе (чуть правее и впереди игрока)
        Vector3 targetPosition = player.position + (player.forward + player.right).normalized * followRadius;
        targetPosition.y += levitateHeight; // Поднимаем позицию для эффекта левитации

        // Плавно перемещаем объект к целевой позиции
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void ResetAbsorbedStatus()
    {
        isAbsorbed = false;
        durachokCollider.enabled = true; // Включаем коллайдер после завершения поглощения
    }
}
