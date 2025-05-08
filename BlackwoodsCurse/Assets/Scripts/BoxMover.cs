using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMover : MonoBehaviour
{
    public HUD hud;
    public GameObject box;
    public GameObject trapdoorTrigger;
    public float moveDistance;
    public float moveSpeed;

    private bool isPlayerNear = false;
    private bool isMoved = false;
    private Vector3 targetPosition;

    void Start()
    {
        // Make sure the trapdoor trigger is initially disabled
        if (trapdoorTrigger != null)
            trapdoorTrigger.SetActive(false);

        // Set the target position to be backward along the Z-axis
        targetPosition = box.transform.position + new Vector3(0, 0, -moveDistance);
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
        {
            MoveBox();
        }
    }

    void MoveBox()
    {
        isMoved = true;
        hud.HideMessage();
        StartCoroutine(SlideBox());

        // Enable the trapdoor interaction
        if (trapdoorTrigger != null)
            trapdoorTrigger.SetActive(true);
    }

    private System.Collections.IEnumerator SlideBox()
    {
        float elapsed = 0f;
        Vector3 startPosition = box.transform.position;

        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * moveSpeed;
            box.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed);
            yield return null;
        }

        box.transform.position = targetPosition; // Snap to final position
        Debug.Log("📦 Box moved, trapdoor is now accessible.");
    }
}
