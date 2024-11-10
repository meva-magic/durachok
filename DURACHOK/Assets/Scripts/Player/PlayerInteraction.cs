using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.Space;  // ������ ��� ��������������
    public float interactionRange = 3f;  // ������ ��� ��������������
    public bool isAttachedToDurachok = false;  // �������� �� � �������

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey) && !isAttachedToDurachok)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
            {
                // �������� ��� PlatformButton
                PlatformButton platformButton = hit.collider.GetComponent<PlatformButton>();
                if (platformButton != null)
                {
                    platformButton.TogglePlatform();
                    return;  // ���� ��� PlatformButton, ����� ��������� �������� � ������� �� ������
                }

                // �������� ��� DoorButton
                DoorButton doorButton = hit.collider.GetComponent<DoorButton>();
                if (doorButton != null)
                {
                    doorButton.ToggleDoor();
                }
            }
        }
    }
}
