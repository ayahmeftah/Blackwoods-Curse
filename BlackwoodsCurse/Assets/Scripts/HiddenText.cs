using UnityEngine;
using TMPro;

public class HiddenText : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public TextMeshProUGUI textMeshPro; // Changed to TextMeshProUGUI for UI Text
    public Light flashlight;

    private float hiddenAlpha = 0f;
    private float visibleAlpha = 1f;

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }

        // Start with invisible text
        Color color = textMeshPro.color;
        color.a = hiddenAlpha;
        textMeshPro.color = color;
    }

    void Update()
    {
        if (flashlight.enabled && flashlight.color == Color.cyan)
        {
            // Reveal the text
            Color color = textMeshPro.color;
            color.a = visibleAlpha;
            textMeshPro.color = color;
        }
        else
        {
            // Hide the text
            Color color = textMeshPro.color;
            color.a = hiddenAlpha;
            textMeshPro.color = color;
        }
    }
}
