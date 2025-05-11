using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public GrimmFloorWanderSync grimmSync;
    public Transform targetForThisFloor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grimmSync.SetWanderTarget(targetForThisFloor);
        }
    }
}