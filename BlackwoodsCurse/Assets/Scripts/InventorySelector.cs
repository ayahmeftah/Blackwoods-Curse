using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InventorySelector : MonoBehaviour
{
    public int currentSlot = 0;
    public int totalSlots = 9;
    public Image[] slotBorders; // Drag the border Images here

    public Color defaultColor = new Color(0f, 0f, 0f, 0.41f);
    public Color selectedColor = Color.red;

    void Start()
    {
        UpdateSelectedSlot();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentSlot = (currentSlot - 1 + totalSlots) % totalSlots;
            UpdateSelectedSlot();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            currentSlot = (currentSlot + 1) % totalSlots;
            UpdateSelectedSlot();
        }
    }

    void UpdateSelectedSlot()
    {
        for (int i = 0; i < slotBorders.Length; i++)
        {
            slotBorders[i].color = (i == currentSlot) ? selectedColor : defaultColor;
        }
    }
}
