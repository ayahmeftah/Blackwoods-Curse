using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlaskMixingManager : MonoBehaviour
{
    [System.Serializable]
    public class Flask
    {
        public string colorName;
        public GameObject flaskObject;
    }

    public List<Flask> flasks;
    public Renderer mainFlaskRenderer;
    public Transform pourTargetPosition;
    public float pourDuration = 2f;
    public TextMeshProUGUI levelCompleteText;
    public CanvasGroup levelCompleteCanvasGroup;
    public PouringZone pouringZone;

    public Material blueMat, yellowMat, redMat;
    public Material greenMat, tealMat, maroonMat;

    public Material wrongColorMat;
    public TextMeshProUGUI hudMessageText;
    public float wrongColorDisplayTime = 2f;

    public GameObject coffinLid;
    public float lidOpenDuration = 5f;

    private string currentColor = "";
    private bool isPouring = false;
    private bool coffinOpened = false;

    private Material originalMaterial;

    public bool IsPouring => isPouring;

    public string cutSceneName = "Cutscene2_p1";

    void Start()
    {
        originalMaterial = mainFlaskRenderer.material;
    }

    public void PourFlaskByColor(string colorName)
    {
        if (isPouring) return;

        GameObject flaskObj = flasks.Find(f => f.colorName == colorName)?.flaskObject;

        if (flaskObj != null)
        {
            StartCoroutine(PourFlaskRoutine(flaskObj, colorName));
        }
    }

    IEnumerator PourFlaskRoutine(GameObject flaskObject, string colorName)
    {
        isPouring = true;

        Vector3 originalPos = flaskObject.transform.position;
        Quaternion originalRot = flaskObject.transform.rotation;

        flaskObject.transform.position = pourTargetPosition.position;
        flaskObject.transform.rotation = Quaternion.Euler(0f, 0f, -90f);

        yield return new WaitForSeconds(pourDuration);

        flaskObject.transform.position = originalPos;
        flaskObject.transform.rotation = originalRot;

        MixColor(colorName);
        isPouring = false;
    }

    void MixColor(string addedColor)
    {
        string result = GetNewMixResult(currentColor, addedColor);

        if (result == "wrong")
        {
            StartCoroutine(HandleWrongColor());
            return;
        }

        currentColor = result;
        UpdateMainFlaskColor(result);

        if (result == "maroon" && !coffinOpened)
        {
            OpenCoffin();

            if (pouringZone != null && pouringZone.interactionText != null)
            {
                pouringZone.interactionText.text = "";
                pouringZone.interactionText.alpha = 0;
                pouringZone.enabled = false;
                pouringZone.GetComponent<Collider>().enabled = false;
            }

            DisplayMessage();
            SceneManager.LoadScene(cutSceneName);
        }
    }

    void UpdateMainFlaskColor(string color)
    {
        switch (color)
        {
            case "blue": mainFlaskRenderer.material = blueMat; break;
            case "yellow": mainFlaskRenderer.material = yellowMat; break;
            case "red": mainFlaskRenderer.material = redMat; break;
            case "green": mainFlaskRenderer.material = greenMat; break;
            case "teal": mainFlaskRenderer.material = tealMat; break;
            case "maroon": mainFlaskRenderer.material = maroonMat; break;
            default:
                ;
                break;
        }
    }

    string GetNewMixResult(string current, string added)
    {
        if (string.IsNullOrEmpty(current)) return added;

        // Valid combinations
        if ((current == "blue" && added == "yellow") || (current == "yellow" && added == "blue"))
            return "green";
        if ((current == "green" && added == "blue") || (current == "blue" && added == "green"))
            return "teal";
        if ((current == "teal" && added == "red") || (current == "red" && added == "teal"))
            return "maroon";

        // Invalid combinations
        if ((current == "red" && added == "blue") || (current == "blue" && added == "red") ||
            (current == "yellow" && added == "red") || (current == "red" && added == "yellow") ||
            (current == "green" && added == "red") || (current == "green" && added == "yellow") ||
            (current == "teal" && added == "blue") || (current == "teal" && added == "yellow"))
            return "wrong";

        if (current == added) return current;

        return current;
    }

    IEnumerator HandleWrongColor()
    {
        mainFlaskRenderer.material = wrongColorMat;
        hudMessageText.text = "Wrong Color!";
        hudMessageText.alpha = 1;

        yield return new WaitForSeconds(wrongColorDisplayTime);

        hudMessageText.alpha = 0;
        mainFlaskRenderer.material = originalMaterial;
        currentColor = ""; // Optionally reset currentColor to clear state
    }

    void OpenCoffin()
    {
        coffinOpened = true;
        StartCoroutine(SlideLidOpen());
    }

    IEnumerator SlideLidOpen()
    {
        Vector3 startPos = coffinLid.transform.position;
        Quaternion startRot = coffinLid.transform.rotation;

        Vector3 targetPos = new Vector3(0.09f, 0.263f, 0.538f);
        Quaternion targetRot = Quaternion.Euler(47.622f, 0f, 0f);

        float elapsed = 0f;

        while (elapsed < lidOpenDuration)
        {
            coffinLid.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / lidOpenDuration);
            coffinLid.transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsed / lidOpenDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        coffinLid.transform.position = targetPos;
        coffinLid.transform.rotation = targetRot;
    }

    Material GetMaterialByColor(string color)
    {
        switch (color)
        {
            case "blue": return blueMat;
            case "yellow": return yellowMat;
            case "red": return redMat;
            case "green": return greenMat;
            case "teal": return tealMat;
            case "maroon": return maroonMat;
            default: return originalMaterial;
        }
    }

    public void DisplayMessage()
    {
        StartCoroutine(DisplayLevelCompleteMessage());
    }

    private IEnumerator DisplayLevelCompleteMessage()
    {
        levelCompleteText.text = "Level Complete!";
        levelCompleteCanvasGroup.alpha = 1;
        yield return new WaitForSeconds(3f);
        levelCompleteCanvasGroup.alpha = 0;
    }
}
