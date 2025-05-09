using UnityEngine;

public class GrimmActivator : MonoBehaviour
{
    public GameObject keyObject;   // Drag the key GameObject here
    public GameObject grimmObject; // Drag the Grimm GameObject here

    private bool grimmEnabled = false;

    void Update()
    {
        if (!grimmEnabled && keyObject != null && !keyObject.activeInHierarchy)
        {
            grimmObject.SetActive(true); // Enable Grimm
            grimmEnabled = true;         // Prevent multiple triggers
        }
    }
}