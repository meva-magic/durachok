using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;
    public PressurePlate plate;
    public DoorButton button;  // ������ �� DoorButton
    public float openDistance = 1.5f;  // ��������� ����� ������ ������������
    public float openSpeed = 2f;
    private bool isOpened = false;

    public void Update()
    {
        // �������� �� ������� ������
        if (plate == null || button == null)
        {
            Debug.LogWarning("Door: ����������� ������ �� plate ��� button.");
            return;
        }

        if (!isOpened && plate.IsActivated() && button.IsActivated())
        {
            AudioManager.instance.Play("DoorOpen");
            
            StartCoroutine(OpenDoors());
            isOpened = true;
        }
    }

    private IEnumerator OpenDoors()
    {
        Vector3 leftTarget = leftDoor.position - leftDoor.right * openDistance;
        Vector3 rightTarget = rightDoor.position + rightDoor.right * openDistance;

        while (Vector3.Distance(leftDoor.position, leftTarget) > 0.01f || Vector3.Distance(rightDoor.position, rightTarget) > 0.01f)
        {
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, leftTarget, openSpeed * Time.deltaTime);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, rightTarget, openSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
