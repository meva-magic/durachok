using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static Vector3 lastCheckpointPosition = new Vector3(3, 1, -2); // Начальная позиция чекпоинта

    // Установка чекпоинта и обновление позиции
    public static void SetCheckpoint(int checkpointNumber)
    {
        switch (checkpointNumber)
        {
            case 0:
                lastCheckpointPosition = new Vector3(3, 1, -2);
                break;
            case 1:
                lastCheckpointPosition = new Vector3(-4, 1, -2);
                break;
            case 2:
                lastCheckpointPosition = new Vector3(-29, 1, -2);
                break;
            default:
                lastCheckpointPosition = Vector3.zero; // Позиция по умолчанию
                break;
        }
    }
}