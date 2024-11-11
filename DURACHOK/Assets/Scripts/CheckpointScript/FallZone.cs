using UnityEngine;

public class FallZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered FallZone.");

            // Отключаем CharacterController, если он есть, чтобы телепортация сработала
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }

            // Определяем новую позицию для игрока из последнего сохранённого чекпоинта
            Vector3 playerNewPosition = CheckpointManager.lastCheckpointPosition;

            // Если позиция не была установлена (например, если чекпоинт 0 не был активирован), устанавливаем дефолтное значение
            if (playerNewPosition == Vector3.zero)
            {
                playerNewPosition = new Vector3(-4, 0, 7); // Позиция по умолчанию
            }

            // Телепортируем игрока
            UIManager.instance.EnabmeDeathMenu();
            other.transform.position = playerNewPosition;

            // Ищем объект Durachok по тегу и телепортируем его рядом с игроком
            GameObject durachok = GameObject.FindWithTag("Durachok");
            if (durachok != null)
            {
                Vector3 durachokNewPosition = playerNewPosition + new Vector3(1, 0, 0); // Смещаем Durachok на 1 единицу по X
                durachok.transform.position = durachokNewPosition;
                Debug.Log("Durachok teleported near the player.");
            }

            // Включаем обратно CharacterController, если он был отключен
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}
