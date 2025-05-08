using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeMagnet : MonoBehaviour, IInventoryItem
{
    public string Name => "RopeMagnet";

    [SerializeField] private Sprite _image;
    public Sprite Image => _image;

    public void OnPickup()
    {
        // This won't be used because it's never in the scene
        gameObject.SetActive(false);
    }
}

