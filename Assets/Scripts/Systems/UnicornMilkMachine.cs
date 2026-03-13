using System;
using DefaultNamespace;
using DefaultNamespace.InventoryBehaviours;
using DefaultNamespace.Systems;
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
        if (inventory.isItemSelected && inventory.items[inventory.selectedItem] != null &&
            inventory.items[inventory.selectedItem] == unicornItem && inventory.HasCapacity())
        {
            

            int slotId = inventory.currentInventoryItem.GetComponent<UnicornIB>().inventorySlot;
            int unicornId = GameManager.Instance.unicornPen.InventoryUnicornIds[slotId];
            UnicornData data = GameManager.Instance.unicornPen.unicorns[unicornId];

            if (Time.time - data.lastMilkingTime < 20f)
            {
                Debug.Log($"Unicorn is not ready to be milked yet!, {20 - (Time.time - data.lastMilkingTime)}s remaining");
                return;
            }
            else
            {
                inventory.Add(unicornMilkItem);
                GameManager.Instance.unicornPen.unicorns[unicornId] = new UnicornData
                {
                    id = data.id,
                    lastMilkingTime = Time.time,
                    hasBody = data.hasBody
                };
            }
        }
    }
}