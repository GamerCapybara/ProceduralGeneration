using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> Slots = new();
    public int SlotsCount;
    
    private void Awake()
    {
        for (;Slots.Count < SlotsCount;)
        {
            Slots.Add(new InventorySlot());
        }
    }

    public void AddItem(InventoryItem inventoryItem, int quantity)
    {
        for (int i = 0; i < SlotsCount; i++)
        {
            // Add item to existing slots with this item
            if (Slots[i].InventoryItem == null || Slots[i].InventoryItem.name != inventoryItem.name) continue;
    
            int freeStackSize = Slots[i].InventoryItem.maxStackSize - Slots[i].Quantity;
            
            // Check free slots
            if (freeStackSize <= 0) continue;
            
            // All items added
            if (quantity <= freeStackSize) 
            {
                Slots[i].Quantity += quantity;
                return; 
            }
            
            // Reduce remaining quantity
            Slots[i].Quantity += freeStackSize;
            quantity -= freeStackSize; 
            
        }
        
        for (int j = 0; j < SlotsCount; j++)
        {
            // Add item if slot is empty
            if (Slots[j].InventoryItem == null)
            {
                // All items added
                Slots[j].InventoryItem = inventoryItem;
                if (quantity <= inventoryItem.maxStackSize) 
                {
                    Slots[j].Quantity = quantity;
                    return; 
                }
                
                // Reduce remaining quantity
                Slots[j].Quantity = inventoryItem.maxStackSize;
                quantity -= inventoryItem.maxStackSize; 
            }
        }
    }

    public void RemoveItem(int index)
    {
        Slots[index] = new InventorySlot();
    }
    
    public void Swap(int fItem, int sItem)
    {
        InventorySlot tInventorySlot = new InventorySlot(Slots[fItem].InventoryItem, Slots[fItem].Quantity);
        
        Slots[fItem] = Slots[sItem];
        Slots[sItem] = tInventorySlot;
    }
}
