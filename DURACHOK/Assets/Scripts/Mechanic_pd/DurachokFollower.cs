using UnityEngine;

public class DurachokFollower : MonoBehaviour
{
    public Transform player;               // Ссылка на игрока
    public float followDistance = 2f;      // Дистанция следования
    public float followSpeed = 2f;         // Скорость следования
    public float levitateHeight = 1f;      // Высота левитации
    public float activationRadius = 5f;    // Радиус для способности

    private bool isFollowing = false;      // Следует ли за игроком
    private bool isAbsorbed = false;       // Находится ли внутри игрока

    private DurachokAbsorption absorptionScript;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Получаем ссылку на компонент DurachokAbsorption
        absorptionScript = GetComponent<DurachokAbsorption>();
        absorptionScript.player = player;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Активация следования при удержании Enter и нахождении в радиусе
        if (Input.GetKeyDown(KeyCode.Return) && distanceToPlayer <= activationRadius)
        {
            isFollowing = true;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            isFollowing = false;
        }

        // Активация способности поглощения при нажатии Space и нахождении в радиусе
        if (Input.GetKeyDown(KeyCode.Space) && distanceToPlayer <= activationRadius && !isAbsorbed)
        {
            isAbsorbed = true;
            StartCoroutine(absorptionScript.AbsorbIntoPlayer());
            Invoke(nameof(ResetAbsorbedStatus), absorptionScript.invisibleDuration + 1f);
        }

        // Следование за игроком, если не в статусе невидимости и не поглощен
        if (isFollowing && !isAbsorbed && !absorptionScript.isInvisible)
        {
            Vector3 targetPosition = player.position - player.forward * followDistance;
            targetPosition.y += levitateHeight;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    void ResetAbsorbedStatus()
    {
        isAbsorbed = false;
    }
}
