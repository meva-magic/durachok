using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, является ли объект, зашедший в коллайдер, игроком
        if (other.CompareTag("Player"))
        {
            // Активируем меню победы
            UIManager.instance.EnableWinMenu();
        }
    }
}
