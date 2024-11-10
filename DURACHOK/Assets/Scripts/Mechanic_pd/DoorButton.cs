using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public Door door;  // Ссылка на дверь
    private bool isActivated = false;  // Статус активации

    public void ToggleDoor()
    {
        isActivated = !isActivated;  // Переключаем статус активации
        if (door != null)
        {
            // Обновляем состояние двери при активации/деактивации
            door.Update();
        }
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
