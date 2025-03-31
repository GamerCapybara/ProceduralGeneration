using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public InventoryUIplayer inventoryUI;
    public InventoryUIplayer hotbarUI;
    public InventoryUItrader traderInventoryUI;

    public void CheckSelectedSlot(IInventoryUI tInventoryUI, int index)
    {
        if ((inventoryUI._selectedSlotIndex is not null || hotbarUI._selectedSlotIndex is not null) && tInventoryUI == traderInventoryUI)
        {
            // if player is trying to put his item in trader's inventory STOP HIM
            inventoryUI._selectedSlotIndex = null;
            hotbarUI._selectedSlotIndex = null;
        }
        else if (inventoryUI._selectedSlotIndex is not null)
        {
            inventoryManager.Swap(tInventoryUI._inventory, inventoryUI._inventory, index, (int)inventoryUI._selectedSlotIndex);
            inventoryUI._selectedSlotIndex = null;
        }
        else if (hotbarUI._selectedSlotIndex is not null)
        {
            inventoryManager.Swap(tInventoryUI._inventory, hotbarUI._inventory, index, (int)hotbarUI._selectedSlotIndex);
            hotbarUI._selectedSlotIndex = null;
        }
        else if (traderInventoryUI._selectedSlotIndex is not null)
        {
            inventoryManager.BuyRequest(tInventoryUI._inventory, traderInventoryUI._inventory, index, (int)traderInventoryUI._selectedSlotIndex);
            traderInventoryUI._selectedSlotIndex = null;
        }
        else if (tInventoryUI._inventory.Slots[index].InventoryItem is not null)
        {
            tInventoryUI._selectedSlotIndex = index;
        }
    }
}
