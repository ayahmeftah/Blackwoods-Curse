using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAccessController : MonoBehaviour
{
    public Collider doorTrigger; // Assign the Box Collider from the door object

    void Start()
    {
        if (doorTrigger != null)
        {
            doorTrigger.enabled = false; // Lock the door initially
        }
    }

    public void EnableDoorTrigger()
    {
        if (doorTrigger != null)
        {
            doorTrigger.enabled = true; // Unlock the door after correct code
        }
    }
}
