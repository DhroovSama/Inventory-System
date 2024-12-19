using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    public class InventorySystem : MonoBehaviour
    {
        [Tooltip("All the inventory slots we currently have.")]
        public List<InventorySlot> slots = new List<InventorySlot>();

        [Tooltip("The maximum number of slots allowed in the inventory.")]
        public int maxSlots = 20;

        #region XML Documentation
        /// <summary>
        /// Attempts to add a certain quantity of an item to the inventory.
        /// If the item is stackable, it will try to add to existing stacks first before creating new slots.
        /// If the item isn't stackable, it will create as many new slots as needed (one per item),
        /// as long as there is space.
        /// </summary>
        /// <param name="itemData">The ItemData representing the type of item to add.</param>
        /// <param name="quantity">How many units of that item to add.</param>
        /// <returns>The remaining quantity that couldn't be added if the inventory lacked space.</returns> 
        #endregion
        public int AddItem(ItemData itemData, int quantity)
        {
            // If the quantity is invalid, do nothing.
            if (quantity <= 0) return 0;

            // Remaining items to add.
            int remainingItems = quantity;

            // If the item is stackable, try to add to existing stacks first.
            if (itemData.isStackable)
            {
                foreach (InventorySlot slot in slots)
                {
                    if (slot.itemData == itemData)
                    {
                        // Space left in the current stack.
                        int spaceInStack = itemData.maxStackSize - slot.quantity;

                        if (spaceInStack > 0)
                        {
                            // Add as many items as possible to this stack.
                            int itemsToAdd = Mathf.Min(remainingItems, spaceInStack);
                            slot.quantity += itemsToAdd;
                            remainingItems -= itemsToAdd;

                            // If all items are placed, return.
                            if (remainingItems <= 0) return 0;
                        }
                    }
                }
            }

            // Add remaining items to new slots if there's space.
            while (remainingItems > 0 && slots.Count < maxSlots)
            {
                int stackSize = itemData.isStackable ? itemData.maxStackSize : 1;

                // Add a new slot with as many items as possible.
                int itemsToAdd = Mathf.Min(remainingItems, stackSize);
                slots.Add(new InventorySlot(itemData, itemsToAdd));
                remainingItems -= itemsToAdd;
            }

            // Return items that couldn't fit.
            return remainingItems;
        }

        #region XML Documentation
        /// <summary>
        /// Removes a specified quantity of an item from the inventory.
        /// </summary>
        /// <param name="itemData">The ItemData representing the type of item to remove.</param>
        /// <param name="quantity">The quantity of the item to remove.</param>
        /// <remarks>
        /// If the quantity in the inventory slot is greater than or equal to the requested quantity, the requested amount is subtracted.
        /// If the quantity in the slot reaches zero or below, the slot is cleared and removed from the inventory.
        /// </remarks>
        #endregion
        public void RemoveItem(ItemData itemData, int quantity)
        {
            InventorySlot slot = slots.Find(s => s.itemData == itemData);
            if (slot != null)
            {
                if (slot.quantity >= quantity)
                {
                    slot.quantity -= quantity;

                    if (slot.quantity <= 0)
                    {
                        slot.ClearSlot();
                        slots.Remove(slot);
                    }
                }
            }
        }


        #region XML Documentation
        /// <summary>
        /// Checks if the inventory has reached its maximum slot capacity.
        /// </summary>
        /// <returns>True if no more slots can be added, false otherwise.</returns> 
        #endregion
        public bool IsFull()
        {
            return slots.Count >= maxSlots;
        }

        #region XML Documentation
        /// <summary>
        /// Removes all items from the inventory, leaving it empty.
        /// </summary> 
        #endregion
        public void ClearInventory()
        {
            slots.Clear();
        }
    }
}
