using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "UnicornGame/InventoryItem", order = 0)]
    public class InventoryItem : ScriptableObject
    {
        public string itemName;
        public Sprite icon;
        public bool stackable;
        
        public GameObject inventoryPrefab;
    }
}