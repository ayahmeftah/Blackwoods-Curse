using UnityEngine;

public class GrimmSwitchToWanderUpstairs : MonoBehaviour
{
    public GrimmBehaviorManager grimmManager;
    public GameObject upstairsArea; // empty object with upstairs bounds

    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered || !other.CompareTag("Grimm")) return;

        triggered = true;

        if (grimmManager != null && upstairsArea != null)
        {
            grimmManager.SwitchToWander(upstairsArea);
            Debug.Log("[Grimm] Switched to upstairs wandering.");
        }
    }
}