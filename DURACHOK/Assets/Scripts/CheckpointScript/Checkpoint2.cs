using UnityEngine;

public class Checkpoint2 : MonoBehaviour
{
    public int checkpointNumber = 2; // Номер текущего чекпоинта

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.SetCheckpoint(checkpointNumber); // Устанавливаем новый чекпоинт
        }
    }
}
