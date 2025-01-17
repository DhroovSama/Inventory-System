using UnityEngine;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Scriptable Object Script for an inventory item, represents their properties.
    /// </summary>
    #endregion
    [CreateAssetMenu(fileName = "ItemData", menuName = "Inventory/ItemData")]
    public class ItemData : ScriptableObject
    {
        [Tooltip("The item's name")]
        public string itemName;

        [Tooltip("A unique ID for this type of item")]
        public int itemID;

        [Tooltip("Sprite IMG that represents this item")] 
        public Sprite icon;

        [Tooltip("The category this item belongs to")]
        public ItemType itemType;

        [Tooltip("Can we stack multiple copies in one slot?")]
        public bool isStackable;

        [Tooltip("If stackable, how many can we stack?")]
        public int maxStackSize;

        [Tooltip("If true then will allow group of items to be dropped with just one prefab")]
        public bool groupedPrefab;

        [Tooltip("Gameobject associated with that item")]
        public GameObject prefab;
    }
}
