using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timeToLive = 2f;
    private float speed = 10f;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }
}
