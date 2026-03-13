using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Pickupable : MonoBehaviour
    {
        public InventoryItem thisItem;
        public UnityEvent actions;
        public InventoryItem PickUp()
        {
            actions.Invoke();
            Destroy(gameObject);
            return thisItem;
        }
    }
}