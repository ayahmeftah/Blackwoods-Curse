using UnityEngine;

public class ParkourFallTrigger : MonoBehaviour
{
    public ParkourFallDetection fallHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fallHandler?.HandleFall(other.transform);
        }
    }
}
