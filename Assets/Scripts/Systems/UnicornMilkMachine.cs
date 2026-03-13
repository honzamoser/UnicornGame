using System;
using DefaultNamespace;
using UnityEngine;

public class UnicornMilkMachine : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem unicornMilkItem;
    public InventoryItem unicornItem;
    private void Start()
    {
        this.inventory = GameManager.Instance.Inventory;
    }

    public void Interact()
    {
        if(inventory.isItemSelected && inventory.items[inventory.selectedItem] != null && inventory.items[inventory.selectedItem] == unicornItem)
        {
            inventory.RemoveCurrentItem();
            GameManager.Instance.unicornPen.animalCount -= 1;
            inventory.Add(unicornMilkItem);
        }
    }
}
