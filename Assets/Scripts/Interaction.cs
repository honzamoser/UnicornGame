using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class Interaction : MonoBehaviour
    {
        public List<GameObject> targetObjects = new List<GameObject>();
        public GameObject InteractButton;

        

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Interactable"))
            {
                targetObjects.Add(other.gameObject);
                InteractButton.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (targetObjects.Contains(other.gameObject))
            {
                targetObjects.Remove(other.gameObject);
            }

            if (targetObjects.Count == 0) InteractButton.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.Instance.disableInteraction)
            {
                PickupInteraction();
            }

            if (targetObjects.Count == 0) return;
            RaycastHit2D[] hit = new RaycastHit2D[1];
            var contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Systems"));
            int closesObjectIndex = 0;
            float closestObjectDistance = Vector2.Distance(transform.position, targetObjects[0].transform.position);

            foreach (var obj in targetObjects)
            {
                var d = Vector2.Distance(transform.position, targetObjects[0].transform.position);
                if (d < closestObjectDistance)
                {
                    closestObjectDistance = d;
                    closesObjectIndex = targetObjects.IndexOf(obj);
                }
            }

            Physics2D.Raycast(transform.position,
                targetObjects[closesObjectIndex].transform.position - transform.position, contactFilter,
                hit, Mathf.Infinity);

            if (hit[0].collider != null)
            {
                InteractButton.SetActive(true);
                InteractButton.transform.position = hit[0].point;

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    Debug.Log(hit[0].collider.name);
                    hit[0].collider.gameObject.SendMessageUpwards("Interact");
                    
                }
            }
        }

        private void PickupInteraction()
        {
            // get the collider under the cursor
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);


            if (hit.collider != null && hit.collider.gameObject.TryGetComponent<Pickupable>(out Pickupable pickupable))
            {
                if (GameManager.Instance.Inventory.HasCapacity())
                {
                    InventoryItem item = pickupable.PickUp();
                    GameManager.Instance.Inventory.Add(item);
                }
            }
        }
    }
}