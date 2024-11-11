using System.Collections;
using UnityEngine;


public class PressurePlate : MonoBehaviour
{
    public GameObject grid;  // ������ �������, ������� ����� ����������
    public float pressDepth = 0.1f;  // �������, �� ������� ����� ����������
    public float moveSpeed = 2f;
    private Vector3 initialPosition;
    private bool isActivated = false;
    private int collidersInside = 0; // ������� �������� ������ ��������

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.Play("PlateOn");

        collidersInside++;

        if (!isActivated) // ���������� ����� ������ ��� ������ �������
        {
            isActivated = true;
            StartCoroutine(MovePlate(initialPosition - Vector3.up * pressDepth));
            StartCoroutine(MoveGridDown());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        AudioManager.instance.Play("PlateOn");
        
        collidersInside--;

        if (collidersInside <= 0) // ������������ �����, ����� ������ �� �������� ��������
        {
            isActivated = false;
            StartCoroutine(MovePlate(initialPosition));
            StartCoroutine(MoveGridUp());
        }
    }

    private IEnumerator MovePlate(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveGridDown()
    {
        Collider gridCollider = grid.GetComponent<Collider>();
        if (gridCollider != null) gridCollider.enabled = false;

        Vector3 targetPosition = grid.transform.position - Vector3.up * 2f;  // �������� �� 2 �����
        while (Vector3.Distance(grid.transform.position, targetPosition) > 0.01f)
        {
            grid.transform.position = Vector3.MoveTowards(grid.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator MoveGridUp()
    {
        Collider gridCollider = grid.GetComponent<Collider>();
        if (gridCollider != null) gridCollider.enabled = true;

        Vector3 targetPosition = grid.transform.position + Vector3.up * 2f;
        while (Vector3.Distance(grid.transform.position, targetPosition) > 0.01f)
        {
            grid.transform.position = Vector3.MoveTowards(grid.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public bool IsActivated()
    {
        return isActivated;
    }
}
