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

            // Определяем новую позицию для игрока в зависимости от чекпоинта
            Vector3 playerNewPosition;
            if (CheckpointManager.checkpoint == 0)
            {
                playerNewPosition = new Vector3(-4, 0, 7);
                Debug.Log("Teleported to checkpoint 0.");
            }
            else if (CheckpointManager.checkpoint == 1)
            {
                playerNewPosition = new Vector3(-7, 0, 4);
                Debug.Log("Teleported to checkpoint 1.");
            }
            else
            {
                // Если чекпоинт неизвестен, выходим
                return;
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
