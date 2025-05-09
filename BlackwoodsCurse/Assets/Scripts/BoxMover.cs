using UnityEngine;
using System.Collections;

public class BoxMover : MonoBehaviour
{
    [Header("References")]
    public HUD hud;
    public GameObject box;
    [Tooltip("Drag your Trapdoor (with the Trapdoor script) here")]
    public Trapdoor trapdoorScript;

    [Header("Movement Settings")]
    public float moveDistance = 0.5f;
    public float moveSpeed = 2f;
    public float liftHeight = 0.02f; 

    [Header("Audio")]
    public AudioSource audioSource;      // Attach an AudioSource component
    public AudioClip slideClip;          // The sound to play while sliding
    public AudioClip dropClip;           // The sound when it hits the floor

    private bool isPlayerNear = false;
    private bool isMoved = false;
    private Vector3 targetPosition;
    private Rigidbody boxRb;

    void Start()
    {
        // Compute final position (lift then slide back)
        targetPosition = box.transform.position + new Vector3(0, liftHeight, -moveDistance);

        // Ensure Rigidbody for drop
        boxRb = box.GetComponent<Rigidbody>();
        if (boxRb == null) boxRb = box.AddComponent<Rigidbody>();
        boxRb.isKinematic = true;
        boxRb.useGravity = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoved)
        {
            isPlayerNear = true;
            hud.txt.text = "Press F to Move the Box";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            hud.HideMessage();
        }
    }

    void Update()
    {
        if (isPlayerNear && !isMoved && Input.GetKeyDown(KeyCode.F))
            MoveBox();
    }

    void MoveBox()
    {
        isMoved = true;
        hud.HideMessage();

        // play the sliding sound once
        if (audioSource != null && slideClip != null)
            audioSource.PlayOneShot(slideClip);

        StartCoroutine(SlideThenDrop());
    }

    private IEnumerator SlideThenDrop()
    {
        // 1) Slide back (lift + slide)
        float t = 0f;
        Vector3 start = box.transform.position;
        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            box.transform.position = Vector3.Lerp(start, targetPosition, t);
            yield return null;
        }
        box.transform.position = targetPosition;

        // 2) Notify the trapdoor
        trapdoorScript?.BoxHasMoved();

        // 3) Let it drop
        boxRb.isKinematic = false;
        boxRb.useGravity = true;

        // play the drop sound
        if (audioSource != null && dropClip != null)
            audioSource.PlayOneShot(dropClip);

        // 4) After it settles, lock it in place
        yield return new WaitForSeconds(0.5f);
        boxRb.isKinematic = true;
        boxRb.useGravity = false;
    }
}
