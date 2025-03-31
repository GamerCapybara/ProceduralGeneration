using System;
using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, .1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Health>() && collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1000);
            StartCoroutine(DestroySelf());
        }
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
