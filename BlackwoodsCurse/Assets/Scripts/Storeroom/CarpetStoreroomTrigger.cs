using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarpetStoreroomTrigger : MonoBehaviour
{
    public GameObject storeroomDoor;
    public Timer timer;  // Reference to the Timer script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (storeroomDoor != null)
            {
                StoreroomDoor doorScript = storeroomDoor.GetComponent<StoreroomDoor>();
                if (doorScript != null)
                {
                    doorScript.CloseAndLockDoor(true);
                }
            }

            // Start the timer
            if (timer != null)
            {
                timer.StartTimer();
            }

            // disable this trigger so it doesn't get retriggered
            GetComponent<Collider>().enabled = false;
        }
    }
}
