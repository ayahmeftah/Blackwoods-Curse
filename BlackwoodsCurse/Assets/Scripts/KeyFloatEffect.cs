using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFloatEffect : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float frequency = 1f;
    private Vector3 startPos;
    private bool hasFloated = false;

    public void SetStartNow()
    {
        startPos = transform.position;
        hasFloated = true;
    }

    void Update()
    {
        if (hasFloated)
        {
            transform.position = startPos + Vector3.up * Mathf.Sin(Time.time * frequency) * amplitude;
        }
    }
}


