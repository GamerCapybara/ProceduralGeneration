using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    GameManager gameManager;
    public TMP_Text permanentCoinsText;

    [SerializeField] TMP_Text HealthUpgrade;
    [SerializeField] TMP_Text BonusDamageUpgrade;
    [SerializeField] TMP_Text RegenerationUpgrade;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        gameManager.PermanentCoins += gameManager.Coins;

        HealthUpgrade.text = gameManager.playerHealthUpgradeCost + "coins";
        BonusDamageUpgrade.text = gameManager.playerBonusDamageUpgradeCost + "coins";
    }

    private void Update()
    {
        permanentCoinsText.text = gameManager.PermanentCoins + " permanent coins";
    }

    public void BuyHealth()
    {
        if (gameManager.PermanentCoins >= gameManager.playerHealthUpgradeCost)
        {
            gameManager.PermanentCoins -= gameManager.playerHealthUpgradeCost;
            gameManager.playerHealthUpgradeCost *= 2;
            HealthUpgrade.text = gameManager.playerHealthUpgradeCost + "coins";

            gameManager.playerHealth += 5;
        }
    }

    public void BuyBonusDamage()
    {
        if (gameManager.PermanentCoins >= gameManager.playerBonusDamageUpgradeCost)
        {
            gameManager.PermanentCoins -= gameManager.playerBonusDamageUpgradeCost;
            gameManager.playerBonusDamageUpgradeCost *= 2;
            BonusDamageUpgrade.text = gameManager.playerBonusDamageUpgradeCost + "coins";
            gameManager.playerBonusDamage += 1;
        }
    }

    public void BuyRegeneration()
    {
        if (gameManager.PermanentCoins >= gameManager.playerRegenerationUpgradeCost)
        {
            gameManager.PermanentCoins -= gameManager.playerRegenerationUpgradeCost;
            gameManager.playerRegenerationUpgradeCost *= 2;
            RegenerationUpgrade.text = gameManager.playerRegenerationUpgradeCost + "coins";

            gameManager.playerRegeneration += 1;
        }
    }
}
