using DefaultNamespace;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem unicornMilkItem;

    private void Start()
    {
        this.inventory = GameManager.Instance.Inventory;
    }

    public void Interact()
    {
        if (inventory.isItemSelected && inventory.items[inventory.selectedItem] != null &&
            inventory.items[inventory.selectedItem] == unicornMilkItem)
        {
            inventory.RemoveCurrentItem();
            GameManager.Instance.Money += 100;
        }
    }
}