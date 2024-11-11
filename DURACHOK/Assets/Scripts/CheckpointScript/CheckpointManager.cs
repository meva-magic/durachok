using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition = new Vector3(-4, 0, 7); // Начальная позиция чекпоинта

    // Установка чекпоинта и обновление позиции
    public static void SetCheckpoint(int checkpointNumber)
    {
        switch (checkpointNumber)
        {
            case 0:
                lastCheckpointPosition = new Vector3(-4, 0, 7);
                break;
            case 1:
                lastCheckpointPosition = new Vector3(-7, 0, 4);
                break;
            default:
                lastCheckpointPosition = Vector3.zero; // Позиция по умолчанию
                break;
        }
    }
}