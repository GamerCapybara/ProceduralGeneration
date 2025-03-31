using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[Serializable]
public class InventorySlot
{
    public InventoryItem InventoryItem;
    public int Quantity;

    public InventorySlot(InventoryItem inventoryItem = null, int quantity = 0)
    {
        InventoryItem = inventoryItem;
        Quantity = quantity;
    }
}