using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CandleMessageDisplay : MonoBehaviour
{
    public Text uiText;
    private Coroutine currentRoutine;

    public void ShowMessage(string message, float duration = 2f)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(Display(message, duration));
    }

    public void HideMessage()
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        uiText.text = "";
        uiText.enabled = false;
    }

    private IEnumerator Display(string message, float duration)
    {
        uiText.text = message;
        uiText.enabled = true;
        yield return new WaitForSeconds(duration);
        uiText.text = "";
        uiText.enabled = false;
    }
}
