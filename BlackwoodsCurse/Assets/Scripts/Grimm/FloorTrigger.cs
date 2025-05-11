using UnityEngine;

public class FloorTrigger : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;
    public GameObject targetArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            grimmManager.SwitchToWander(targetArea);
        }
    }
}