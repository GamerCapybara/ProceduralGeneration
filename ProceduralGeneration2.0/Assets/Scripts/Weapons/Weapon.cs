using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public string TARGET_TAG = "Enemy";
    public bool canBeDestroyed = false;
    public bool isExplosive = false;
    [CanBeNull] public GameObject explosion;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() && collision.CompareTag(TARGET_TAG))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            if (isExplosive) Instantiate(explosion, transform.position, Quaternion.identity);
            if (canBeDestroyed) Destroy(gameObject);
        }
    }
}
