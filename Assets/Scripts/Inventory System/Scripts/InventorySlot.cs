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

        private ItemButtonSettings itemButton;

        #region XML Documentation
        /// <summary>
        /// Constructor for initializing the slot with a specific item and quantity.
        /// </summary>
        /// <param name="id">The ItemData reference for the item.</param>
        /// <param name="q">The quantity of the item.</param> 
        #endregion
        public InventorySlot(ItemData id, int q)
        {
            itemData = id;        
            quantity = q;          
            itemButton = InventoryPanelManager.Instance.CreateInventoryButton(itemData);
        }

        #region XML Documentation
        /// <summary>
        /// Determines if the slot is considered empty.
        /// A slot is empty if there is no itemData or if quantity is 0 or less.
        /// </summary>
        /// <returns>True if empty, false otherwise.</returns> 
        #endregion
        public bool IsEmpty()
        {
            return itemData == null || quantity <= 0;
        }

        #region XML Documentation
        /// <summary>
        /// Clears the slot by removing the item reference and resetting quantity to zero.
        /// </summary> 
        #endregion
        public void ClearSlot()
        {
            itemData = null;
            quantity = 0;
        }

        #region XML Documentation
        /// <summary>
        /// Sets a new item and quantity for this slot.
        /// Useful when changing items or updating their count.
        /// </summary>
        /// <param name="newItemData">The new ItemData to place in the slot.</param>
        /// <param name="newQuantity">The new quantity of the item.</param> 
        #endregion
        public void SetItem(ItemData newItemData, int newQuantity)
        {
            itemData = newItemData;
            quantity = newQuantity;
        }
    }
}
