using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreroomDoorLock : MonoBehaviour
{
    public bool isLocked = true;

    public void TryOpenDoor()
    {
        if (isLocked)
        {
            Debug.Log("The door is locked!");

        }
        else
        {
            Debug.Log("The door opens.");

        }
    }

    public void UnlockDoor()
    {
        isLocked = false;
        Debug.Log("The door has been unlocked!");
    }
}
