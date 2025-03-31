using System.Collections.Generic;
using UnityEngine;

public interface IInventoryUI
{
    public InventoryUIManager _inventoryUIManager { get; set; }
    public Inventory _inventory { get; set; }
    public GameObject _inventorySlot { get; set; }

    public List<GameObject> _inventorySlots { get; set; }
    public int? _selectedSlotIndex { get; set; }
    public void UpdateUI();
}
