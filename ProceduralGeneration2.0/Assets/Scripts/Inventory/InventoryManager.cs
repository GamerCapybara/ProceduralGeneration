using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // public Inventory inventory;
    // public Inventory hotbar;
    public GameManager gameManager;
    
    public void Swap(Inventory fInventory, Inventory sInventory, int fItem, int sItem)
    {
        InventorySlot tInventorySlot = new InventorySlot(fInventory.Slots[fItem].InventoryItem, fInventory.Slots[fItem].Quantity);
        
        fInventory.Slots[fItem] = sInventory.Slots[sItem];
        sInventory.Slots[sItem] = tInventorySlot;
    }

    public void BuyRequest(Inventory fInventory, Inventory traderInventory, int fItem, int traderItem)
    {
        if (gameManager.Coins >= traderInventory.Slots[traderItem].InventoryItem.buyPrice)
        {

            if (fInventory.Slots[fItem].InventoryItem is null)
            {
                Buy(fInventory, traderInventory, fItem, traderItem);
                return;
            }

            for (int i = 0; i < fInventory.Slots.Count; i++)
            {
                if (fInventory.Slots[i].InventoryItem is null)
                {
                    Buy(fInventory, traderInventory, i, traderItem);
                    return;
                }
            }
        }
    }

    private void Buy(Inventory fInventory, Inventory traderInventory, int fItem, int traderItem)
    {
        gameManager.Coins -= traderInventory.Slots[traderItem].InventoryItem.buyPrice;
        fInventory.Slots[fItem].InventoryItem = traderInventory.Slots[traderItem].InventoryItem;
        fInventory.Slots[fItem].Quantity = 1;
    }
}
