using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static int checkpoint = 0; // ������� ��������
    public static Vector3 lastCheckpointPosition; // ������� ���������� ���������

    // ����� ��� ��������� ��������� � ��� �������
    public static void SetCheckpoint(int checkpointNumber)
    {
        checkpoint = checkpointNumber;

        // � ����������� �� ������ ��������� ������������� �������
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
            lastCheckpointPosition = Vector3.zero; // ����� �������� ��� ������ ����������
        }
    }
}
