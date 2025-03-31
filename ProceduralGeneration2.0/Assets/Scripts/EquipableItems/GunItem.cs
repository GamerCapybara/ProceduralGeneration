using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GunItem : MonoBehaviour, IItem
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = .3f;
    private bool canShoot = true;
    
    private void Update()
    {
        // Get the position of the mouse in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction vector from the object to the mouse
        Vector3 direction = mousePosition - transform.position;

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

        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    private IEnumerator Shoot()
    {
        canShoot = false;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    public void Use()
    {
        
    }

    public GameObject Equip(Transform playerTransform, Inventory hotbar, int index)
    {
        GameObject itemObject = hotbar.Slots[index].InventoryItem.itemObject;
        itemObject = Instantiate(itemObject, Vector3.zero, Quaternion.identity);
        itemObject.transform.SetParent(playerTransform, false);
        return itemObject;
    }

    public void UnEquip()
    {
        Destroy(gameObject);
    }
}
