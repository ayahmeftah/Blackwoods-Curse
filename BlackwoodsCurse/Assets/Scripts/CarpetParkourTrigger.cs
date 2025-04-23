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
            //deactivating the floor 
            if (floor != null) floor.SetActive(false);

            //hiding the dining room door
            if (diningRoomDoor != null) diningRoomDoor.SetActive(false);

            //enabling floating furnitures
            foreach (GameObject obj in objectsToFloat)
            {
                if (obj.TryGetComponent(out Rigidbody rb))
                {
                    rb.useGravity = false;
                    rb.isKinematic = false;
                    rb.AddForce(Vector3.up * 2f, ForceMode.Impulse);
                }
            }

            //disable this trigger so it doesn't re-fire again
            gameObject.SetActive(false);
        }
    }
}
