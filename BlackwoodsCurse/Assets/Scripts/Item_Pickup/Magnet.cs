using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Magnet";
        }
    }

    [SerializeField]
    private Sprite _image;
    public Sprite Image
    {
        get
        {
            return _image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }
}