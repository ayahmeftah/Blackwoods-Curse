using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiningDoorLock : MonoBehaviour
{
    public bool isLocked = true;

    public void TryOpenDoor()
    {
        if (isLocked)
        {
            Debug.Log("The door is locked...");
            // Optionally play locked sound here
        }
        else
        {
            Debug.Log("The door opens.");
            // Call your real open-door logic here
        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        Debug.Log("The door has been unlocked!");
    }
}

