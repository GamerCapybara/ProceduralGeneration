using System;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public Inventory hotbar;

    private GameObject? equipedItem = null;
    private int equipedItemIndex = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (equipedItem is not null)
            {
                equipedItem.GetComponent<IItem>().UnEquip();
                equipedItem = null;
            }
            if(hotbar.Slots[0].InventoryItem is not null)
            {
                equipedItem = hotbar.Slots[0].InventoryItem.itemObject.GetComponent<IItem>().Equip(transform, hotbar, 0);
            }
            equipedItemIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (equipedItem is not null)
            {
                equipedItem.GetComponent<IItem>().UnEquip();
                equipedItem = null;
            }
            if(hotbar.Slots[1].InventoryItem is not null)
            {
                equipedItem = hotbar.Slots[1].InventoryItem.itemObject.GetComponent<IItem>().Equip(transform, hotbar, 1);
            }
            equipedItemIndex = 1;
        }
        if (hotbar.Slots[equipedItemIndex].InventoryItem is null && equipedItem is not null)
        {
            equipedItem.GetComponent<IItem>().UnEquip();
            equipedItem = null;
        }
        if (hotbar.Slots[equipedItemIndex].InventoryItem is not null && equipedItem is null)
        {
            equipedItem = hotbar.Slots[equipedItemIndex].InventoryItem.itemObject.GetComponent<IItem>().Equip(transform, hotbar, equipedItemIndex);
        }
    }
}
