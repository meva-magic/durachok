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

            // ���������� ����� ������� ��� ������ � ����������� �� ���������
            Vector3 playerNewPosition;
            if (CheckpointManager.checkpoint == 0)
            {
                playerNewPosition = new Vector3(-4, 0, 7);
                Debug.Log("Teleported to checkpoint 0.");
            }
            else if (CheckpointManager.checkpoint == 1)
            {
                playerNewPosition = new Vector3(-7, 0, 4);
                Debug.Log("Teleported to checkpoint 1.");
            }
            else
            {
                // ���� �������� ����������, �������
                return;
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
