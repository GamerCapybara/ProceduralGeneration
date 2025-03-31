using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int health = 30;
    public int maxHealth = 30;
    public int regeneration = 0;

    public FloatingHealthBar healthBar;
    public PlayManager playManager;
    private void Awake()
    {
        playManager = FindAnyObjectByType<PlayManager>();
        if (healthBar == null) healthBar = GetComponentInChildren<FloatingHealthBar>();
        healthBar.UpdateHealthBar(health, maxHealth);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (transform.CompareTag("Player"))
            {
                SceneManager.LoadScene(0);
                return;
            }
            
            playManager.gameManager.Coins += 3;
            Destroy(gameObject);
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    private float time = 0f;
    private void Update()
    {
        if (!transform.CompareTag("Player")) return;

        if (time >= 1f)
        {
            health += regeneration;
            healthBar.UpdateHealthBar(health, maxHealth);
            time = 0f;
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
