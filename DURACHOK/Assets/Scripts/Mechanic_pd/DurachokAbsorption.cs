using UnityEngine;
using System.Collections;

public class DurachokAbsorption : MonoBehaviour
{
    public Transform player;               // Ссылка на игрока
    public float arcHeight = 2f;           // Высота дуги
    public float invisibleDuration = 5f;   // Длительность невидимости
    private Vector3 originalScale;         // Начальный размер для восстановления
    private Collider durachokCollider;     // Коллайдер Дурачка

    public bool isInvisible = false;       // Статус невидимости

    public float respawnRadius = 2f;       // Радиус для случайной позиции рядом с игроком после невидимости

    void Start()
    {
        originalScale = transform.localScale;
        // Получаем ссылку на коллайдер Дурачка
        durachokCollider = GetComponent<Collider>();
    }

    public IEnumerator AbsorbIntoPlayer()
    {
        // Выключаем коллайдер на время способности
        if (durachokCollider != null)
        {
            durachokCollider.enabled = false;
        }

        // Запоминаем текущую позицию Дурачка
        Vector3 startPosition = transform.position;
        Vector3 endPosition = player.position;
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero; // Уменьшаем до 0

        float time = 0f;
        while (time < 1f)
        {
            // Двигаем по дуге от текущей позиции к игроку
            Vector3 arcPosition = Vector3.Lerp(startPosition, endPosition, time);
            arcPosition.y += Mathf.Sin(time * Mathf.PI) * arcHeight;
            transform.position = arcPosition;

            // Плавно уменьшаем размер
            transform.localScale = Vector3.Lerp(startScale, endScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        // Активируем невидимость
        isInvisible = true;
        transform.localScale = endScale;

        // Следуем за позицией игрока во время невидимости
        float invisibleTime = 0f;
        while (invisibleTime < invisibleDuration)
        {
            transform.position = player.position;
            invisibleTime += Time.deltaTime;
            yield return null;
        }

        // После завершения невидимости
        // Появляемся в случайной позиции рядом с игроком в пределах радиуса
        Vector3 randomOffset = new Vector3(Random.Range(-respawnRadius, respawnRadius), 0f, Random.Range(-respawnRadius, respawnRadius));
        Vector3 randomPosition = player.position + randomOffset;

        // Начинаем анимацию выпрыгивания из случайной позиции рядом с игроком
        time = 0f;
        Vector3 exitPosition = randomPosition; // Начальная позиция для выпрыгивания

        while (time < 1f)
        {
            // Двигаем по дуге от случайной позиции рядом с игроком
            Vector3 arcPosition = Vector3.Lerp(exitPosition, randomPosition, time); // Выпрыгиваем в случайную точку
            arcPosition.y += Mathf.Sin(time * Mathf.PI) * arcHeight;
            transform.position = arcPosition;

            // Восстанавливаем размер
            transform.localScale = Vector3.Lerp(endScale, originalScale, time);
            time += Time.deltaTime;
            yield return null;
        }

        // Включаем коллайдер обратно после завершения выпрыгивания
        if (durachokCollider != null)
        {
            durachokCollider.enabled = true;
        }

        // Завершаем процесс
        isInvisible = false;
    }

    public bool IsInvisible()
    {
        return isInvisible;
    }
}
