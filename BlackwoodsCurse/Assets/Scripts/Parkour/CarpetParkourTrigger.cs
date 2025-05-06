using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetParkourTrigger : MonoBehaviour
{
    public GameObject[] objectsToFloat;
    public GameObject floor;
    public GameObject diningRoomDoor;
    public GameObject voidPlane;
    public GameObject safeStartPlatform;
    public ParkourFallDetection fallDetectionHandler;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start the supernatural event after a short delay
            StartCoroutine(StartSupernaturalEvent());

            // disable this trigger so it doesn't get retriggered
            GetComponent<Collider>().enabled = false;
        }
            // Start fall detection after short delay
        if (fallDetectionHandler != null)
        {
            fallDetectionHandler.ActivateDetectionWithDelay();
        }
    }

    private IEnumerator StartSupernaturalEvent()
    {
        // Wait before starting the supernatural event
        yield return new WaitForSeconds(3f);

        if (voidPlane != null) voidPlane.SetActive(true);
        if (safeStartPlatform != null) safeStartPlatform.SetActive(true);

        // Hide floor
        if (floor != null) floor.SetActive(false);

        if (diningRoomDoor != null)
        {
            DiningDoor doorScript = diningRoomDoor.GetComponent<DiningDoor>();
            if (doorScript != null)
            {
                doorScript.CloseAndLockDoor(true);
            }

        }

        // Enable floating furniture (temporary physics)
        foreach (GameObject obj in objectsToFloat)
        {
            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.useGravity = false;
                rb.isKinematic = false;
                rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
            }
        }

        //wait till objects float
        yield return new WaitForSeconds(1f);

        foreach (GameObject obj in objectsToFloat)
        {
            FloatToTarget ft = obj.GetComponent<FloatToTarget>();
            if (ft != null)
                ft.activateFloat = true;
        }

        // Freeze furniture in place
        foreach (GameObject obj in objectsToFloat)
        {
            if (obj.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = Vector3.zero;    // Stop any movement
                rb.angularVelocity = Vector3.zero; // Stop rotation
                rb.isKinematic = true;         // Freeze it
            }
        }
        //disable this trigger so it doesn't re-fire again
        gameObject.SetActive(false);
    }

}
