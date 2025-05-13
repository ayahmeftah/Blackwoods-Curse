using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootLight : MonoBehaviour
{
    public Material material;
    LightBeam beam;
    public GameObject storeroomDoor;
    public TextMeshProUGUI levelCompleteText;
    public CanvasGroup levelCompleteCanvasGroup;

    public CanvasGroup MirrorTextCanvasGroup;

    public static ShootLight Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        Destroy(GameObject.Find("Light Beam"));
        beam = new LightBeam(gameObject.transform.position, gameObject.transform.right, material);
    }

    public void HideMirrorText()
    {
        MirrorTextCanvasGroup.alpha = 0;
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
