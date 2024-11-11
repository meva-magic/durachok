using UnityEngine;

public class FallZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered FallZone.");

            // Телепортируем игрока и Дурочка на последнюю сохранённую позицию чекпоинта
            TeleportPlayerAndDurachok();
        }
    }

    private void TeleportPlayerAndDurachok()
    {
        Vector3 playerNewPosition = CheckpointManager.lastCheckpointPosition;

        // Телепортируем игрока
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) controller.enabled = false;
            player.transform.position = playerNewPosition;
            if (controller != null) controller.enabled = true;
        }

        // Телепортируем Дурочка
        GameObject durachok = GameObject.FindGameObjectWithTag("Durachok");
        if (durachok != null)
        {
            durachok.transform.position = playerNewPosition + new Vector3(1, 0, 0); // Смещаем на 1 единицу по X
            Debug.Log("Durachok teleported near the player.");
        }

        UIManager.instance.EnabmeDeathMenu();
    }
}