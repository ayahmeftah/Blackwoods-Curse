using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CarpetStoreroomTrigger : MonoBehaviour
{
    public GameObject storeroomDoor;
    public Timer timer;      

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

            // Disable this trigger so it doesn't get retriggered
            GetComponent<Collider>().enabled = false;
        }
    }
}
