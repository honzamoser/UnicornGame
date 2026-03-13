using NPC;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class UnityIntEvent : UnityEvent<int> { }
    public class Pickupable : MonoBehaviour
    {
        public InventoryItem thisItem;
        public UnityEvent beforePickup;
        public UnityIntEvent afterPickup;
        
        public UnicornBehaviour thisUnicorn;
        public InventoryItem PickUp()
        {
            beforePickup.Invoke();
            return thisItem;
        }

        public void AfterPickup(int slotLocation)
        {
            // afterPickup.Invoke(slotLocation);
            if (thisUnicorn != null)
            {
                this.thisUnicorn.AfterRemove(slotLocation);
            }

            Destroy(gameObject);
        }
    }
}