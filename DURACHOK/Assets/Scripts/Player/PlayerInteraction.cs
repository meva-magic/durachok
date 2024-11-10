using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public KeyCode interactionKey = KeyCode.Space;  // Кнопка для взаимодействия
    public float interactionRange = 3f;  // Радиус для взаимодействия
    public bool isAttachedToDurachok = false;  // Привязан ли к Дурочку

    private void Update()
    {
        if (Input.GetKeyDown(interactionKey) && !isAttachedToDurachok)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
            {
                // Проверка для PlatformButton
                PlatformButton platformButton = hit.collider.GetComponent<PlatformButton>();
                if (platformButton != null)
                {
                    platformButton.TogglePlatform();
                    return;  // Если это PlatformButton, сразу выполняем действие и выходим из метода
                }

                // Проверка для DoorButton
                DoorButton doorButton = hit.collider.GetComponent<DoorButton>();
                if (doorButton != null)
                {
                    doorButton.ToggleDoor();
                }
            }
        }
    }
}
