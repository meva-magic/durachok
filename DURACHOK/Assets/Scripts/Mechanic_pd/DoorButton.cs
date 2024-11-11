using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Door door;  // ������ �� �����
    private bool isActivated = false;  // ������ ���������

    public void ToggleDoor()
    {
        isActivated = !isActivated;  // ����������� ������ ���������
        if (door != null)
        {
            AudioManager.instance.Play("LeverPress");
            
            // ��������� ��������� ����� ��� ���������/�����������
            door.Update();
        }
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
