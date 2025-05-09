using UnityEngine;

public class GrimmBarrierRemover : MonoBehaviour
{
    public GameObject barrierObject; // Assign GrimmStairsBarrier here

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && barrierObject != null)
        {
            barrierObject.SetActive(false); // Let Grimm pass now
            Debug.Log("Grimm barrier removed — upstairs unlocked.");
        }
    }
}