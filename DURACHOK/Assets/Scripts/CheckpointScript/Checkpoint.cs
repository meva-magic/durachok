using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber = 1; // ����� �������� ���������

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckpointManager.checkpoint = checkpointNumber;
        }
    }

}
