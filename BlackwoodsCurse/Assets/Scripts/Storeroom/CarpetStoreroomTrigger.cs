using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarpetStoreroomTrigger : MonoBehaviour
{
    public GameObject storeroomDoor;

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

            // disable this trigger so it doesn't get retriggered
            GetComponent<Collider>().enabled = false;
        }
    }
}