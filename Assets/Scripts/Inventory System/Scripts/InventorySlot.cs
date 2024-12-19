using UnityEngine;

namespace Inventory.sys
{
    [System.Serializable]
    public class InventorySlot
    {
        [Tooltip("The type of item stored here")]
        public ItemData itemData;
        [Tooltip("How many of that item we have")]
        public int quantity;       

        public InventorySlot(ItemData id, int q)
        {
            itemData = id;         // Set the initial item type
            quantity = q;          // Set how many of them we start with
        }

        public bool IsEmpty()
        {
            // If we don't have an item or no quantity, this slot is empty
            return itemData == null || quantity <= 0;
        }

        public void ClearSlot()
        {
            // Remove any item from this slot
            itemData = null;
            quantity = 0;
        }

        public void SetItem(ItemData newItemData, int newQuantity)
        {
            // Update this slot with a new item and amount
            itemData = newItemData;
            quantity = newQuantity;
        }
    }
}
