using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    private Transform player;
    public float shootingRange = 2f;
    public float fireRate = 2f;
    private bool canShoot = true;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootingRoutine());
    }

    private void Update()
    {
        // Get the position of the mouse in world space
        Vector3 playerPos = player.position;

        // Calculate the direction vector from the object to the mouse
        Vector3 direction = playerPos - transform.position;

        // Set the z-axis rotation angle based on the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the object
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private IEnumerator ShootingRoutine()
    {
        while (true)
        {
            if (player != null && Vector3.Distance(transform.position, player.position) <= shootingRange)
            {
                if (canShoot)
                {
                    Shoot();
                }
            }
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        canShoot = false;
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        canShoot = true;
    }
}
