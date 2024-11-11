using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static int checkpoint = 0; // Текущий чекпоинт
    public static Vector3 lastCheckpointPosition; // Позиция последнего чекпоинта

    // Метод для установки чекпоинта и его позиции
    public static void SetCheckpoint(int checkpointNumber)
    {
        checkpoint = checkpointNumber;

        // В зависимости от номера чекпоинта устанавливаем позицию
        if (checkpointNumber == 0)
        {
            lastCheckpointPosition = new Vector3(-4, 0, 7);
        }
        else if (checkpointNumber == 1)
        {
            lastCheckpointPosition = new Vector3(-7, 0, 4);
        }
        else
        {
            lastCheckpointPosition = Vector3.zero; // Можно добавить для других чекпоинтов
        }
    }
}
