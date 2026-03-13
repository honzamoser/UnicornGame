using System;
using DefaultNamespace;
using UnityEngine;

public class UnicornMilkMachine : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem unicornMilkItem;
    private void Start()
    {
        this.inventory = GameManager.Instance.Inventory;
    }

    public void Interact()
    {
        if(inventory.isItemSelected && inventory.items[inventory.selectedItem] != null && inventory.items[inventory.selectedItem].name.Contains("Unicorn"))
        {
            inventory.RemoveCurrentItem();
            inventory.Add(unicornMilkItem);
        }
    }
}
