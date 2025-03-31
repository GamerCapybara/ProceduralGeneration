using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InventoryUIplayer : MonoBehaviour, IInventoryUI
{
    public InventoryUIManager _inventoryUIManager { get => inventoryUIManager; set => inventoryUIManager = value; }
    public Inventory _inventory { get => inventory; set => inventory = value; }
    public GameObject _inventorySlot { get => inventorySlot; set => inventorySlot = value; }

    public List<GameObject> _inventorySlots { get => inventorySlots; set => inventorySlots = value; }
    public int? _selectedSlotIndex { get => SelectedSlotIndex; set => SelectedSlotIndex = value; }


    public InventoryUIManager inventoryUIManager;
    public Inventory inventory;
    public GameObject inventorySlot;

    public List<GameObject> inventorySlots = new();
    public int? SelectedSlotIndex;
    
    private void Start()
    {
        for (int i = 0; i < inventory.SlotsCount; i++)
        {
            GameObject slot = Instantiate(inventorySlot, transform);
            int index = i;
            
            slot.GetComponent<Button>().onClick.AddListener(() => OnSlotClicked(index));
            
            inventorySlots.Add(slot);
        }
        
        UpdateUI();
    }
    private void Update()
    {
        UpdateUI(); //temp
    }

    public void UpdateUI()
    {
        for (int i = 0; i < inventory.SlotsCount; i++)
        {
            if (inventory.Slots[i].InventoryItem == null)
            {
                inventorySlots[i].transform.Find("Icon").gameObject.SetActive(false);
                inventorySlots[i].transform.Find("Quantity").gameObject.SetActive(false);
                continue;
            }
            
            inventorySlots[i].transform.Find("Icon").GetComponent<Image>().sprite = inventory.Slots[i].InventoryItem.icon;
            inventorySlots[i].transform.Find("Quantity").GetComponent<TMP_Text>().text = inventory.Slots[i].Quantity == 1 ? "" : inventory.Slots[i].Quantity.ToString();
            
            inventorySlots[i].transform.Find("Icon").gameObject.SetActive(true);
            inventorySlots[i].transform.Find("Quantity").gameObject.SetActive(true);
        }
    }

    private void OnSlotClicked(int index)
    {
        if (SelectedSlotIndex is null)
        {
            inventoryUIManager.CheckSelectedSlot(this, index);
        }
        else
        {
            inventoryUIManager.CheckSelectedSlot(this, index);
            SelectedSlotIndex = null;
        }
        


        // if (_selectedSlotIndex == null)
        // {
        //     if (inventory.Slots[index].InventoryItem == null) return;
        //     // No slot is selected, select this slot
        //     _selectedSlotIndex = index;
        // }
        // else
        // {
        //     // A slot was already selected, swap it with the clicked slot
        //     inventory.Swap(_selectedSlotIndex.Value, index);
        //     _selectedSlotIndex = null;
        //
        //     // Update the UI to reflect the changes
        //     UpdateUI();
        // }
    }
}
