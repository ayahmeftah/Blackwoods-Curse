using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedroomKey : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "BedroomKey";
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
    // Hide the key
    gameObject.SetActive(false);

    // Disable vase glow
    CombineHandler combiner = FindObjectOfType<CombineHandler>();
    if (combiner != null)
    {
        combiner.DisableVaseGlow();
    }
}

void Update()
{
    if (Input.GetKeyDown(KeyCode.K))
    {
        Debug.Log("Current key name: " + Name);
    }
}



}
