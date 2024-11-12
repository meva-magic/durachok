using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 1f; // Задержка перед падением
    public float fallSpeed = 5f; // Скорость падения
    private bool isFalling = false; // Флаг для предотвращения повторного падения

    private Rigidbody rb;

    void Start()
    {
        // Получаем компонент Rigidbody
        rb = GetComponent<Rigidbody>();

        // Делаем платформу неподвижной в начале
        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Игрок наступил на платформу");
        if (other.CompareTag("Player") && !isFalling)
        {
            StartCoroutine(StartFalling());
        }
    }



    IEnumerator StartFalling()
    {
        isFalling = true;

        // Ждем заданную задержку перед началом падения
        yield return new WaitForSeconds(fallDelay);

        // Включаем физику, убирая isKinematic
        rb.isKinematic = false;

        // Используем linearVelocity для задания начальной скорости падения
        rb.linearVelocity = Vector3.down * fallSpeed;
    }
}
