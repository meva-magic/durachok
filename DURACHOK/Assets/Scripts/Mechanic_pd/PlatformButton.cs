using UnityEngine;

public class PlatformButton : MonoBehaviour
{
    public MovingPlatform platform;  // ������ �� ���������

    public void TogglePlatform()
    {
        if (platform != null)
        {
            platform.ToggleMovement();  // ����� ������������ ���������
        }
    }
}
