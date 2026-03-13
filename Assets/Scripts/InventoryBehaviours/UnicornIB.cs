using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace.InventoryBehaviours
{
    public class UnicornIB : MonoBehaviour
    {
        public GameObject unicornPrefab;
        public int inventorySlot;

        public void SetId(int id)
        {
            this.inventorySlot = id;
        }

        private void Start()
        {
            GameManager.Instance.disableInteraction = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                GameManager.Instance.unicornPen.SpawnUnicornFromInventory(mousePos, inventorySlot);
                GameManager.Instance.disableInteraction = false;
                GameManager.Instance.Inventory.RemoveCurrentItem();
            }
        }

        private void OnDestroy()
        {
            GameManager.Instance.disableInteraction = false;
        }
    }
}