using UnityEngine;
using System.Collections;

public class DurachokAbsorption : MonoBehaviour
{
    public GameObject timer;
    public Transform player;
    public float arcHeight = 2f;
    public float invisibleDuration = 5f;
    public float activationRadius = 2f; // Радиус активации способности
    public float timeLeft;
    private Vector3 originalScale;
    private Collider durachokCollider;

    public bool isInvisible = false;
    public float respawnRadius = 2f;
    public bool canHide = true; // Флаг для блокировки способности

    public static DurachokAbsorption instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalScale = transform.localScale;
        durachokCollider = GetComponent<Collider>();
    }

    void Update()
    {
        // Проверка нажатия Enter, расстояния и доступности способности
        if (canHide && Input.GetKeyDown(KeyCode.Return) && Vector3.Distance(transform.position, player.position) <= activationRadius)
        {
            StartCoroutine(AbsorbIntoPlayer());
        }
    }

    public IEnumerator AbsorbIntoPlayer()
    {
        if (!canHide) yield break; // Прерываем, если способность недоступна

        canHide = false; // Блокируем активацию до выпрыгивания обратно

        // Отключаем коллайдер и делаем невидимым
        if (durachokCollider != null)
        {
            durachokCollider.enabled = false;
        }
        isInvisible = true;

        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;

        // Анимация прыжка по дуге и уменьшение размера
        float time = 0f;
        while (time < 1f)
        {
            Vector3 arcPosition = Vector3.Lerp(startPosition, endPosition, time);
            arcPosition.y += Mathf.Sin(time * Mathf.PI) * arcHeight;
            transform.position = arcPosition;
            transform.localScale = Vector3.Lerp(startScale, endScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        // Включаем таймер и удерживаем позицию
        timer.SetActive(true);
        timeLeft = invisibleDuration;
        float invisibleTime = 0f;
        while (invisibleTime < invisibleDuration)
        {
            transform.position = player.position;
            invisibleTime += Time.deltaTime;
            timeLeft = invisibleDuration - invisibleTime;
            yield return null;
        }

        // Позиция для вылета
        Vector3 randomOffset = new Vector3(
            Random.Range(-respawnRadius, respawnRadius),
            0f,
            Random.Range(-respawnRadius, respawnRadius)
        );
        Vector3 spawnPosition = player.position + randomOffset;

        // Вылет по дуге и восстановление размера
        time = 0f;
        transform.localScale = endScale;
        while (time < 1f)
        {
            Vector3 arcPosition = Vector3.Lerp(player.position, spawnPosition, time);
            arcPosition.y += Mathf.Sin(time * Mathf.PI) * arcHeight;
            transform.position = arcPosition;
            transform.localScale = Vector3.Lerp(endScale, originalScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        // Включаем коллайдер и отключаем таймер
        if (durachokCollider != null)
        {
            durachokCollider.enabled = true;
        }
        timer.SetActive(false);
        isInvisible = false;
        canHide = true; // Разрешаем активацию
    }

    public bool IsInvisible()
    {
        return isInvisible;
    }
}
