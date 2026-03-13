using System;
using DefaultNamespace;
using DefaultNamespace.InventoryBehaviours;
using DefaultNamespace.Systems;
using TMPro;
using UnityEngine;

public class UnicornMilkMachine : MonoBehaviour
{
    public Inventory inventory;
    public InventoryItem unicornMilkItem;
    public InventoryItem unicornItem;
    
    public TextMeshPro statusText;
    public float statusDisplayTime = 0f;

    private void Start()
    {
        this.inventory = GameManager.Instance.Inventory;
    }

    private void Update()
    {
        if (statusDisplayTime > 0f)
        {
            statusDisplayTime -= Time.deltaTime;
            if (statusDisplayTime <= 0f)
            {
                statusText.text = "";
            }
        }
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
                statusText.text = $"Unicorn is not ready to be milked yet!, {Math.Ceiling(20 - (Time.time - data.lastMilkingTime))}s remaining";
                statusDisplayTime = 2f;
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