using UnityEngine;

public class FallZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered FallZone.");

            // ��������� CharacterController, ���� �� ����, ����� ������������ ���������
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }

            // ���������� ����� ������� ��� ������ �� ���������� ����������� ���������
            Vector3 playerNewPosition = CheckpointManager.lastCheckpointPosition;

            // ���� ������� �� ���� ����������� (��������, ���� �������� 0 �� ��� �����������), ������������� ��������� ��������
            if (playerNewPosition == Vector3.zero)
            {
                playerNewPosition = new Vector3(-4, 0, 7); // ������� �� ���������
            }

            // ������������� ������
            UIManager.instance.EnabmeDeathMenu();
            other.transform.position = playerNewPosition;

            // ���� ������ Durachok �� ���� � ������������� ��� ����� � �������
            GameObject durachok = GameObject.FindWithTag("Durachok");
            if (durachok != null)
            {
                Vector3 durachokNewPosition = playerNewPosition + new Vector3(1, 0, 0); // ������� Durachok �� 1 ������� �� X
                durachok.transform.position = durachokNewPosition;
                Debug.Log("Durachok teleported near the player.");
            }

            // �������� ������� CharacterController, ���� �� ��� ��������
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}
