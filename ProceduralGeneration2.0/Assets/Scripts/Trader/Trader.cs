using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trader : MonoBehaviour
{
    private Player player;
    public GameObject inventoryUI;
    public GameManager gameManager;
    public Inventory inventory;
    public List<InventorySlot> slots = new();
    private int _slotsCount = 24;

    private void Start()
    {
        for (; slots.Count < _slotsCount;)
        {
            slots.Add(new InventorySlot());
        }

        slots[0].InventoryItem = gameManager.items[Random.Range(0, gameManager.items.Count)];


    player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (_playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            inventory.Slots = slots;
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    private bool _playerInRange = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInRange = true;
        player.ToggleInteractionPanel();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        _playerInRange = false;
        player.ToggleInteractionPanel();
        inventoryUI.SetActive(false);
    }
}
