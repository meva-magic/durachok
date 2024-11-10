using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 2.5f;
    [SerializeField] private float turnSpeed = 1.5f;
    [SerializeField] private float damping = 1.5f;

    [SerializeField] private CharacterController controller;

    private Vector3 inputVector;
    private Vector3 movementVector;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetInput();
        RotatePlayer();
        MovePlayer();
    }

    private void GetInput()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVector.Normalize();
        
        if (inputVector.magnitude >= 0.1f)
        {
            movementVector = inputVector * playerSpeed;
        }
        else
        {
            movementVector = Vector3.Lerp(movementVector, Vector3.zero, Time.deltaTime * damping);
        }
    }

    private void RotatePlayer()
    {
        if (inputVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }
    }

    private void MovePlayer()
    {
        controller.Move(movementVector * Time.deltaTime);
    }
}

