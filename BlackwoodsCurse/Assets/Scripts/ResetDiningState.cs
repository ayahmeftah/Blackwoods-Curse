using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDiningState : MonoBehaviour
{
[System.Serializable]
public class FloatingObject
{
    public GameObject obj;
    [HideInInspector] public Vector3 originalPosition;
    [HideInInspector] public Quaternion originalRotation;

    public void StoreOriginalTransform()
    {
        if (obj != null)
        {
            originalPosition = obj.transform.position;
            originalRotation = obj.transform.rotation;
        }
    }
}


    public List<FloatingObject> floatingObjects;
    public GameObject blackVoid;
    public GameObject startPlatform;
    public Door roomDoor;
    public GameObject diningFloorGroup;
    public GameObject smallCandle;

  void Start()
{
    foreach (FloatingObject fo in floatingObjects)
    {
        fo.StoreOriginalTransform();
        Debug.Log($"Stored original transform for: {fo.obj.name}");
    }
}

    public void RestoreRoom()
    {
        // 1. Return floating objects to their original position
foreach (var item in floatingObjects)
{
    if (item.obj != null)
    {
        item.obj.transform.position = item.originalPosition;
        item.obj.transform.rotation = item.originalRotation;

        // Stop physics movement
        Rigidbody rb = item.obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            // rb.isKinematic = true;  // Freeze it
        }

        // Disable floating script if one exists
        MonoBehaviour floatingScript = item.obj.GetComponent<FloatToTarget>(); // Replace with actual script name
        if (floatingScript != null)
        {
            floatingScript.enabled = false;
        }
    }
}


        // 2. Disable black void and starting platform
        if (blackVoid != null) blackVoid.SetActive(false);
        if (startPlatform != null) startPlatform.SetActive(false);

        diningFloorGroup.SetActive(true);

        if (smallCandle != null)
        smallCandle.SetActive(false);


        // 3. Unlock and open the exit door
        if (roomDoor != null)
        {
            roomDoor.isLocked = false;
            roomDoor.ForceOpen();
        }

        Debug.Log("Room restored and normalized.");
    }
}
