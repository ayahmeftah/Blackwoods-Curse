using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigCandle : MonoBehaviour
{
    public GameObject flameObject;
    public int candleIndex;
    public bool IsLit { get; private set; } = false;

    public void LightCandle()
    {
        if (!IsLit && flameObject != null)
        {
            flameObject.SetActive(true);
            IsLit = true;
            Debug.Log("Candle " + candleIndex + " lit.");
        }
    }

    public void ResetCandle()
    {
        if (flameObject != null)
            flameObject.SetActive(false);

        IsLit = false;
    }
}
