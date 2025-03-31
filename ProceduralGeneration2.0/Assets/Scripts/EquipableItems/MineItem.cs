using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineItem : MonoBehaviour, IItem
{
    public float fireRate = .6f;
    private bool canPlace = true;
    public GameObject minePrefab;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canPlace)
            {
                StartCoroutine(Place());
            }
        }
    }

    private IEnumerator Place()
    {
        canPlace = false;
        Instantiate(minePrefab, transform.position, transform.rotation);
        
        yield return new WaitForSeconds(fireRate);
        canPlace = true;
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
