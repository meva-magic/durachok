using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuController menuController; // Reference to the MenuController

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Notify the MenuController that this button is hovered
        menuController.UpdateArrowPosition(this.transform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Optionally handle pointer exit if needed
    }
}
