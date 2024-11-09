using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public MovingPlatform platform;  // —сылка на платформу

    public void TogglePlatform()
    {
        if (platform != null)
        {
            platform.ToggleMovement();
        }
    }
}
