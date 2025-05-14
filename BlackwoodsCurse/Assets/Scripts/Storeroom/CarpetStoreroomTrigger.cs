using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarpetStoreroomTrigger : MonoBehaviour
{
    public GameObject storeroomDoor;
    public Timer timer;
    public float delayBeforeClose = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (storeroomDoor != null)
            {
                //StoreroomDoor doorScript = storeroomDoor.GetComponent<StoreroomDoor>();
                //if (doorScript != null)
                //{
                //    doorScript.CloseAndLockDoor(true);
                //}
                var doorScript = storeroomDoor.GetComponent<StoreroomDoorAyah>();
                if (doorScript != null)
                {
                    StartCoroutine(DelayedClose(doorScript));
                }
            }

            // Start the timer
            if (timer != null)
            {
                timer.StartTimer();
            }

            // Disable this trigger so it doesn't get retriggered
            GetComponent<Collider>().enabled = false;
        }
    }

    private IEnumerator DelayedClose(StoreroomDoorAyah doorScript)
    {
        yield return new WaitForSeconds(delayBeforeClose);
        doorScript.AutoCloseAndLock(); // Close and lock after delay
    }
}
