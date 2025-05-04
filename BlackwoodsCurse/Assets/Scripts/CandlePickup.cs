using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandlePickup : MonoBehaviour
{
    public Transform holdPoint; //assigned to player’s hand
    public float lightDistance = 2.5f;
    private bool isHolding = false;
    private bool inRange = false;
    public CandlePuzzleManager puzzleManager;  
    public Transform flameTip; 


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

// void TryLightNearbyCandle()
// {
//     if (!isHolding) return;

//     Vector3 origin = flameTip.position;
//     Vector3 direction = flameTip.forward;

//     Debug.DrawRay(origin, direction * lightDistance, Color.red, 1f); // Optional for debugging

//     if (Physics.Raycast(origin, direction, out RaycastHit hit, lightDistance))
//     {
//         BigCandle candle = hit.collider.GetComponentInParent<BigCandle>();
//         if (candle != null && !candle.IsLit)
//         {
//             candle.LightCandle();
//             puzzleManager.CandleLit(candle.candleIndex);
//         }
//     }
// }
void TryLightNearbyCandle()
{
    if (!isHolding || flameTip == null) return;

    Vector3 origin = flameTip.position;

    // Use the forward direction of the candle based on its world rotation
    Vector3 direction = flameTip.TransformDirection(Vector3.forward);

    Debug.DrawRay(origin, direction * lightDistance, Color.red, 2f);

    if (Physics.Raycast(origin, direction, out RaycastHit hit, lightDistance))
    {
        Debug.Log("Ray hit: " + hit.collider.name);

        BigCandle candle = hit.collider.GetComponentInParent<BigCandle>();
        if (candle != null && !candle.IsLit)
        {
            candle.LightCandle();
            puzzleManager.CandleLit(candle.candleIndex);
        }
    }
}



}
