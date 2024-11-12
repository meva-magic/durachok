using UnityEngine;

public class Checkpoint2 : MonoBehaviour
{
    public int checkpointNumber = 2; // ����� �������� ���������

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.SetCheckpoint(checkpointNumber); // ������������� ����� ��������
        }
    }
}
