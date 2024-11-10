using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button[] buttons; // Array to hold your buttons
    public RectTransform arrow; // Reference to the arrow RectTransform
    private int currentIndex = 0; // Index of the currently selected button

    void Start()
    {
        // Set the initial position of the arrow
        UpdateArrowPosition();
    }

    void Update()
    {
        // Navigate through buttons using arrow keys
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
        if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    private void UpdateArrowPosition()
    {
        // Update the position of the arrow based on the selected button
        Vector3 buttonPosition = buttons[currentIndex].transform.position;
        arrow.position = new Vector3(buttonPosition.x, buttonPosition.y, buttonPosition.z); // Adjust offset as needed
    }

    // Overloaded method to update arrow position based on hovered button
    public void UpdateArrowPosition(Transform buttonTransform)
    {
        // Update the current index based on the hovered button
        currentIndex = System.Array.IndexOf(buttons, buttonTransform.GetComponent<Button>());
        UpdateArrowPosition();
    }
}
