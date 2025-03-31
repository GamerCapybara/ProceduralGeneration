using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public PlayManager playManager;
    
    public GameObject InventoryUI;
    public GameObject InteractionPanel;
    
    public Health Health;

    [SerializeField] Tilemap WaterTileMap;

    private void Start()
    {        
        
        Health.health = playManager.gameManager.playerHealth;
        Health.maxHealth = playManager.gameManager.playerHealth;
        Health.regeneration = playManager.gameManager.playerRegeneration;
    }

    private void Update()
    {
        while (IsWater(transform.position))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        }
        ToggleInventoryUI();
    }

    private void ToggleInventoryUI()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) InventoryUI.SetActive(!InventoryUI.activeSelf);
    }

    public void ToggleInteractionPanel()
    {
        InteractionPanel.SetActive(!InteractionPanel.activeSelf);
    }

    bool IsWater(Vector3 position)
    {
        Vector3Int gridPosition = WaterTileMap.WorldToCell(position);
        TileBase tile = WaterTileMap.GetTile(gridPosition);
        if (tile != null)
        {
            return true;
        }
        return false;
    }

    // resources

    public Tilemap tilemap;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tilemap != null)
        {
            Vector3 playerPos = transform.position;
            Vector3 hitPos = collision.ClosestPoint(playerPos);

            Vector3 direction = (hitPos - playerPos).normalized;

            Vector3 adjustedPos = hitPos + (direction * 0.1f);
            Vector3Int cellPosition = tilemap.WorldToCell(adjustedPos);

            if (tilemap.HasTile(cellPosition))
            {
                tilemap.SetTile(cellPosition, null);

                playManager.gameManager.Coins += 2;
            }
        }
    }
}
