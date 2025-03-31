using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    public GameManager gameManager;
    
    public TMP_Text CoinText;
    
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        gameManager.Coins = 0;
    }
    
    private void Update()
    {
        if(CoinText is not null)
            CoinText.text = gameManager.Coins + " Coins";
    }
}
