using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickup : MonoBehaviour
{
    public Transform holdPoint; // Assigned from player’s hand/head
    public float lightDistance = 2f;
    private bool isHolding = false;
    private bool inRange = false;
    public CandlePuzzleManager puzzleManager;  // assign it in Inspector

    void Start()
    {
        puzzleManager = FindObjectOfType<CandlePuzzleManager>();
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.H))
        {
            HoldCandle();
        }

        if (isHolding && Input.GetKeyDown(KeyCode.L))
        {
            TryLightNearbyCandle();
        }
    }

    void HoldCandle()
    {
        isHolding = true;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inRange = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inRange = false;
    }

void TryLightNearbyCandle()
{
    // Temporarily disable collider of the held candle
    Collider ownCollider = GetComponent<Collider>();
    if (ownCollider != null) ownCollider.enabled = false;

    Vector3 rayOrigin = Camera.main.transform.position;
    Vector3 rayDirection = Camera.main.transform.forward;

    if (Physics.Raycast(rayOrigin, rayDirection, out RaycastHit hit, lightDistance))
    {
        Debug.Log("Ray hit: " + hit.collider.name);

        BigCandle candle = hit.collider.GetComponentInParent<BigCandle>();
        if (candle != null && !candle.IsLit)
        {
            candle.LightCandle();
            puzzleManager.CandleLit(candle.candleIndex);
        }
    }

    // Re-enable collider after raycast
    if (ownCollider != null) ownCollider.enabled = true;
}



}
