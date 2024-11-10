using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 10f;
    [SerializeField] private float damping = 5f;
    [SerializeField] private float gravity = -9.81f;  // Гравитация для игрока

    private CharacterController controller;
    private Vector3 inputVector;
    private Vector3 movementVector;
    private Vector3 velocity;
    public static PlayerMove instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
        ApplyGravity();
        MovePlayer();
    }

    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            inputVector.Normalize();  // Нормализуем движение, чтобы не было ускорения по диагонали
            inputVector = transform.TransformDirection(inputVector);  // Преобразуем относительно ориентации игрока
        }
        else
        {
            inputVector = Vector3.Lerp(inputVector, Vector3.zero, Time.deltaTime * damping);  // Замедление при отсутствии ввода
        }

        movementVector = inputVector * playerSpeed;
    }

    private void ApplyGravity()
    {
        if (controller.isGrounded)
        {
            velocity.y = -2f;  // Легкое давление вниз, чтобы игрок не "плавал" в воздухе
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;  // Применение гравитации, если игрок в воздухе
        }
    }

    private void MovePlayer()
    {
        // Объединяем движение и гравитацию
        movementVector.y = velocity.y;
        controller.Move(movementVector * Time.deltaTime);
    }
}
