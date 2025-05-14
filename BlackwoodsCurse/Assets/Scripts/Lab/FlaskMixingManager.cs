using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlaskMixingManager : MonoBehaviour
{
    [System.Serializable]
    public class Flask
    {
        public string colorName;
        public GameObject flaskObject;
    }

    //Color Mixing Logic variables
    public List<Flask> flasks;
    public Renderer mainFlaskRenderer;
    public Transform pourTargetPosition;
    public float pourDuration = 5f;
    public TextMeshProUGUI levelCompleteText;
    public CanvasGroup levelCompleteCanvasGroup;
    public PouringZone pouringZone;  // Assign this in the Inspector

    public Material blueMat, yellowMat, redMat;
    public Material greenMat, tealMat, maroonMat;

    private string currentColor = "";
    private bool isPouring = false;

    public bool IsPouring => isPouring; // So the trigger script can check

    //For Coffin opening variables
    public GameObject coffinLid;
    public float lidOpenDuration = 5f;

    private bool coffinOpened = false;

    //Wrong Color Logic variables
    public Material wrongColorMat; 
    public TextMeshProUGUI hudMessageText; 
    public float wrongColorDisplayTime = 2f;

    private string lastValidColor = ""; 


    public void PourFlaskByColor(string colorName)
    {
        if (isPouring) return;

        GameObject flaskObj = flasks.Find(f => f.colorName == colorName)?.flaskObject;

        if (flaskObj != null)
        {
            StartCoroutine(PourFlaskRoutine(flaskObj, colorName));
        }
        else
        {
            Debug.LogWarning("Flask not found for color: " + colorName);
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
        string previousColor = currentColor;
        string result = GetNewMixResult(currentColor, addedColor);

        if (result == "wrong")
        {
            StartCoroutine(HandleWrongColor());
            return;
        }

        lastValidColor = result;
        currentColor = result;
        UpdateMainFlaskColor(result);

        if (result == "maroon" && !coffinOpened)
        {
            OpenCoffin();
            
            // Hide instructions
            if (pouringZone != null && pouringZone.interactionText != null)
            {
                pouringZone.interactionText.text = "";
                pouringZone.interactionText.alpha = 0;
                pouringZone.enabled = false;
                pouringZone.GetComponent<Collider>().enabled = false;
            }

            //Show level completed message 
            DisplayMessage();
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
            default: Debug.Log("Unknown color: " + color); break;
        }
    }

    string GetNewMixResult(string current, string added)
    {
        if (string.IsNullOrEmpty(current)) return added;

        //Detect right combos
        if ((current == "blue" && added == "yellow") || (current == "yellow" && added == "blue"))
            return "green";
        if ((current == "green" && added == "blue") || (current == "blue" && added == "green"))
            return "teal";
        if ((current == "teal" && added == "red") || (current == "red" && added == "teal"))
            return "maroon";

        //Detect wrong color combos
        if ((current == "red" && added == "blue") || (current == "blue" && added == "red") ||
            (current == "yellow" && added == "red") || (current == "red" && added == "yellow"))
            return "wrong";

        if (current == added) return current;

        return current;
    }

    void OpenCoffin()
    {
        coffinOpened = true;
        Debug.Log("Coffin opens...");

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

    IEnumerator HandleWrongColor()
    {
        Debug.Log("Wrong color mixed!");

        mainFlaskRenderer.material = wrongColorMat;
        hudMessageText.text = "Wrong Color!";
        hudMessageText.alpha = 1;

        yield return new WaitForSeconds(wrongColorDisplayTime);

        hudMessageText.alpha = 0;
        mainFlaskRenderer.material = GetMaterialByColor(lastValidColor);
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
            default: return null;
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
        yield return new WaitForSeconds(3f); // Display for 3 seconds
        levelCompleteCanvasGroup.alpha = 0;
    }
}
