using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber = 1; // Номер текущего чекпоинта

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.SetCheckpoint(checkpointNumber); // Устанавливаем новый чекпоинт
        }
    }
}