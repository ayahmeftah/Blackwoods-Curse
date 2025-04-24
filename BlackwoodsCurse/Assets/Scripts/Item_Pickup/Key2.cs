using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key2 : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Key2";
        }
    }

    public Sprite _Image = null;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

}
