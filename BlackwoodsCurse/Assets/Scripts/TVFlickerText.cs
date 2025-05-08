using TMPro;
using UnityEngine;

public class TVFlickerText : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float speed = .5f; // how fast it fades
    public float minAlpha = 0.1f;
    public float maxAlpha = 1f;

    void Update()
    {
        if (tmpText != null)
        {
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * speed, 1));
            Color color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;
        }
    }
}
