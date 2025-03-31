using UnityEngine;

public interface IItem
{
    public GameObject Equip(Transform playerTransform, Inventory hotbar, int index);
    public void UnEquip();
}
