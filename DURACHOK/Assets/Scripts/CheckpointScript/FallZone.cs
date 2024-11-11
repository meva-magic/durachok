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
            GameObject durachok = GameObject.FindWithTag("Durachok");
            Vector3 playerNewPosition;
            if (CheckpointManager.checkpoint == 0)
            {
                playerNewPosition = new Vector3(-4, 0, 7);
                if (durachok != null)
                {
                    Vector3 durachokNewPosition = new Vector3(-4, 1, 8);
                    durachok.transform.position = durachokNewPosition;
                    Debug.Log("Durachok teleported near the player.");
                }

            }
            else if (CheckpointManager.checkpoint == 1)
            {
                playerNewPosition = new Vector3(-7, 0, 4);
                if (durachok != null)
                {
                    Vector3 durachokNewPosition = new Vector3(-7, 1, 5);
                    durachok.transform.position = durachokNewPosition;
                    Debug.Log("Durachok teleported near the player.");
                }
            }
            else
            {
                // Если чекпоинт неизвестен, выходим
                return;
            }

            // Телепортируем игрока
            other.transform.position = playerNewPosition;

            // Включаем обратно CharacterController, если он был отключен
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}
