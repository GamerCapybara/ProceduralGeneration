using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item", menuName = "Inventory/Inventory Item")]
public class InventoryItem : ScriptableObject
{
    public new string name;
    public Sprite icon;
    public int maxStackSize = 1;
    public int buyPrice;
    public int sellPrice;
    public GameObject itemObject;
}
