using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    
    public List<InventoryItem> items;
    public int playerHealth = 50;
    public int playerHealthUpgradeCost = 5;

    public int playerBonusDamage = 0;
    public int playerBonusDamageUpgradeCost = 5;

    public int playerRegeneration = 0;
    public int playerRegenerationUpgradeCost = 5;

    public int Coins;
    public int PermanentCoins;

    private void Awake()
    {
        if (_instance is not null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}