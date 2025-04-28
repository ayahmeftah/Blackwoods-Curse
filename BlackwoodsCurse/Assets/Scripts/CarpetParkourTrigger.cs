using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpetParkourTrigger : MonoBehaviour
{
    public GameObject[] objectsToFloat;
    public GameObject floor;
    public GameObject diningRoomDoor;


    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        // Start the supernatural event after a short delay
        StartCoroutine(StartSupernaturalEvent());
        
        // Optionally: disable this trigger so it doesn't get retriggered
        GetComponent<Collider>().enabled = false;
    }
}

// private IEnumerator StartSupernaturalEvent()
// {
//     // Wait for 3 seconds (you can change the time if you want)
//     yield return new WaitForSeconds(3f);

//     // Hide floor
//     if (floor != null) floor.SetActive(false);

//     // Hide door
//     if (diningRoomDoor != null) diningRoomDoor.SetActive(false);

//     // Enable floating furniture
//     foreach (GameObject obj in objectsToFloat)
//     {
//         if (obj.TryGetComponent(out Rigidbody rb))
//         {
//             rb.useGravity = false;
//             rb.isKinematic = false;
//             rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
//         }
//     }

//   //disable this trigger so it doesn't re-fire again
//          gameObject.SetActive(false);
// }

private IEnumerator StartSupernaturalEvent()
{
    // Wait before starting the supernatural event
    yield return new WaitForSeconds(3f);

    // Hide floor
    if (floor != null) floor.SetActive(false);

    // Hide door
    if (diningRoomDoor != null) diningRoomDoor.SetActive(false);

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

    // Wait a bit while they float (adjust time if needed)
    yield return new WaitForSeconds(1f);

    // Freeze furniture in place
    foreach (GameObject obj in objectsToFloat)
    {
        if (obj.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.zero;    // Stop any movement
            rb.angularVelocity = Vector3.zero; // Stop rotation
            rb.isKinematic = true;         // Freeze it!
        }
    }
    //disable this trigger so it doesn't re-fire again
      gameObject.SetActive(false);
}

}
