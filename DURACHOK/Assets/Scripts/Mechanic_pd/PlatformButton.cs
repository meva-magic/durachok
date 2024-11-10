using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public MovingPlatform platform;  // Ссылка на платформу

    public void TogglePlatform()
    {
        if (platform != null)
        {
            platform.ToggleMovement();  // Вызов переключения платформы
        }
    }
}
