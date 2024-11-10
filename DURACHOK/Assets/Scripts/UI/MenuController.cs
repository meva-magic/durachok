using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button[] buttons;
    public RectTransform arrow;
    private int currentIndex = 0;

    void Start()
    {
        UpdateArrowPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
            UpdateArrowPosition();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            currentIndex = (currentIndex + 1) % buttons.Length;
            UpdateArrowPosition();
        }

        // Execute button on Enter key press
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    private void UpdateArrowPosition()
    {
        Vector3 buttonPosition = buttons[currentIndex].transform.position;
        arrow.position = new Vector3(buttonPosition.x, buttonPosition.y, buttonPosition.z); 
    }

    public void UpdateArrowPosition(Transform buttonTransform)
    {
        // Update the current index based on the hovered button
        currentIndex = System.Array.IndexOf(buttons, buttonTransform.GetComponent<Button>());
        UpdateArrowPosition();
    }
}
