using UnityEngine;

public class ChestDrop : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasLanded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = true;
    }

    public void Drop()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        hasLanded = false;
    }

    void OnCollisionEnter(Collision collision)
{
    if (!hasLanded && collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    {
        LockChest();
    }
}


    private void LockChest()
    {
        hasLanded = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
        Debug.Log("✅ Chest locked in place after landing.");
    }
}
