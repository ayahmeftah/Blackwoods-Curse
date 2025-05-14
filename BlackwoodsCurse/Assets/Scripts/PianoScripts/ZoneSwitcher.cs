using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneSwitcher : MonoBehaviour
{
    public int activeZone = 1;

    public Image zone1Image;
    public Image zone2Image;
    public Image zone3Image;

    public Color activeColor = new Color(1f, 1f, 0f, 0.4f); // Light yellow
    public Color inactiveColor = new Color(1f, 1f, 1f, 0f);  // Fully transparent

    void Start()
    {
        UpdateZoneUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            activeZone = 1;
            UpdateZoneUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            activeZone = 2;
            UpdateZoneUI();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            activeZone = 3;
            UpdateZoneUI();
        }
    }

    void UpdateZoneUI()
    {
        zone1Image.color = (activeZone == 1) ? activeColor : inactiveColor;
        zone2Image.color = (activeZone == 2) ? activeColor : inactiveColor;
        zone3Image.color = (activeZone == 3) ? activeColor : inactiveColor;
    }
}
