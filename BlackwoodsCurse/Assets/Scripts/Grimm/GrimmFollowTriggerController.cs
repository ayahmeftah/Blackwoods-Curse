using UnityEngine;

public class GrimmFollowTriggerController : MonoBehaviour
{
    public GameObject followTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grimm") && followTrigger != null)
        {
            followTrigger.SetActive(false);
            Debug.Log("[FollowTriggerController] Disabled trigger: " + followTrigger.name);
        }
    }
}