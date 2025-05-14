using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandlePickup : MonoBehaviour
{
    public Transform holdPoint; // assigned to player’s hand
    public float lightDistance = 2f;
    private bool isHolding = false;
    private bool inRange = false;

    public CandlePuzzleManager puzzleManager;
    public Transform flameTip;
    public AudioSource audioSource;
    public AudioClip lightCandleClip;

    private CandleMessageDisplay messageDisplay;

    void Start()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, 1f);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                inRange = true;
                break;
            }
        }

        puzzleManager = FindObjectOfType<CandlePuzzleManager>();
        messageDisplay = FindObjectOfType<CandleMessageDisplay>();

        if (messageDisplay == null)
            Debug.LogWarning("CandleMessageDisplay not found in scene!");
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.H))
        {
            HoldCandle();
            messageDisplay?.ShowMessage("Press L to light a candle",5f);
        }

        if (isHolding && !inRange)
        {
            messageDisplay?.ShowMessage("Press L to light a candle",5f);
        }

        if (inRange && Input.GetKeyDown(KeyCode.L))
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
        {
            inRange = true;
            if (!isHolding)
            {
                messageDisplay?.ShowMessage("Press H to hold the candle",5f);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
            if (!isHolding)
                messageDisplay?.HideMessage();
        }
    }

    void TryLightNearbyCandle()
    {
        if (!isHolding || flameTip == null) return;

        Vector3 origin = flameTip.position;
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

                if (audioSource != null && lightCandleClip != null)
                    audioSource.PlayOneShot(lightCandleClip);
            }
        }
    }
}
