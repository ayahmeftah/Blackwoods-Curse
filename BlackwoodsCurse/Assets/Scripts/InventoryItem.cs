using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }
    void OnPickup();
}

public class InventoryEventArgs : EventArgs
{
    public IInventoryItem Item { get; }
    public int SlotIndex { get; }

    public InventoryEventArgs(IInventoryItem item, int slotIndex)
    {
        Item = item;
        SlotIndex = slotIndex;
    }
}

