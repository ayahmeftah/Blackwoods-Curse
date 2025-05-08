using UnityEngine;

public class SafeDoor : MonoBehaviour
{
    public static bool IsSafeFullyOpened = false;

    public Collider bloodVialCollider;

    private void Start()
    {
        // Deactivate bloodVial collider at start
        if (bloodVialCollider != null)
            bloodVialCollider.enabled = false;
    }

    // Called by animation event
    public void SafeDoorOpened()
    {
        Debug.Log("SafeDoorOpened event triggered from animation.");

        StartCoroutine(EnablePickupAfterDelay(1f));
    }

    private System.Collections.IEnumerator EnablePickupAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        IsSafeFullyOpened = true;

        if (bloodVialCollider != null)
            bloodVialCollider.enabled = true;

        Debug.Log("Safe is fully opened. BloodVial collider now active.");
    }
}
