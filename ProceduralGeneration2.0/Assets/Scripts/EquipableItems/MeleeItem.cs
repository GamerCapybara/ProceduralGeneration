using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeItem : MonoBehaviour, IItem
{
    
    public GameObject weaponHolder;

    public bool isAttacking = false;
    public float swingDuration = 0.3f;
    public float swingAngle = 110f;

    private void Update()
    {
        if (!isAttacking)
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
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!isAttacking)
            {
                StartCoroutine(SwordSwing());
            }
        }
    }

    private IEnumerator SwordSwing()
    {
        isAttacking = true;

        float angle = transform.rotation.eulerAngles.z;
        if (angle > 90 && angle < 270)
        {
            float initialAngle = weaponHolder.transform.rotation.eulerAngles.z - swingAngle / 2;

            // Set to start position
            weaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialAngle));

            float backStartTime = Time.time;
            float targetAngle = initialAngle + swingAngle;

            // Swing forward
            while (Time.time < backStartTime + swingDuration)
            {
                float t = (Time.time - backStartTime) / (swingDuration);
                float currentAngle = Mathf.Lerp(initialAngle, targetAngle, t);
                weaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
                yield return null;
            }

            // Return to default state
            weaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + swingAngle / 2));
            isAttacking = false;
        }
        else
        {
            float initialAngle = weaponHolder.transform.rotation.eulerAngles.z + swingAngle / 2;

            // Set to start position
            weaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, initialAngle));

            float backStartTime = Time.time;
            float targetAngle = initialAngle - swingAngle;

            // Swing forward
            while (Time.time < backStartTime + swingDuration)
            {
                float t = (Time.time - backStartTime) / (swingDuration);
                float currentAngle = Mathf.Lerp(initialAngle, targetAngle, t);
                weaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, currentAngle));
                yield return null;
            }

            // Return to default state
            weaponHolder.transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle + swingAngle / 2));
            isAttacking = false;
        }
        
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
