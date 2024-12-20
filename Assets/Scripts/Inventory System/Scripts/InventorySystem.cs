using JetBrains.Annotations;
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
        /// <param name="removePartial">
        /// If true, allows partial removal of items when the total available quantity is less than the requested amount.
        /// If false, no items will be removed if the total available quantity is insufficient.
        /// </param>
        /// <returns>
        /// The remaining quantity of the item that couldn't be removed. 
        /// If all items are removed successfully, returns 0.
        /// </returns>
        /// <remarks>
        /// This method iterates through the inventory slots containing the specified item and attempts to remove the specified quantity.
        /// It handles cases where the quantity spans multiple slots.
        /// </remarks>
        #endregion
        public int RemoveItem(ItemData itemData, int quantity, bool removePartial = true)
        {
            // Keep track of how many items still need to be removed.
            int remainingItems = quantity;

            // Find all inventory slots that contain the specified item.
            List<InventorySlot> slotsWithItem = new List<InventorySlot>();
            foreach (InventorySlot slot in slots)
            {
                if (slot.itemData == itemData)
                {
                    slotsWithItem.Add(slot);
                }
            }

            // Calculate the total number of the specified item available in the inventory.
            int totalAvailableItems = 0;
            foreach (InventorySlot slot in slotsWithItem)
            {
                totalAvailableItems += slot.quantity;
            }

            // If there aren't enough items to remove and partial removal isn't allowed, abort and return the requested quantity.
            if (remainingItems > totalAvailableItems && !removePartial)
            {
                return quantity;
            }

            // Start removing items from the inventory slots.
            foreach (var slot in slotsWithItem)
            {
                // If no more items need to be removed, exit the loop.
                if (remainingItems <= 0)
                    break;

                // Check if the current slot has fewer items than the remaining quantity to remove.
                if (slot.quantity < remainingItems)
                {
                    // Subtract the slot's quantity from the remaining items.
                    remainingItems -= slot.quantity;

                    // Clear the slot and remove it from the inventory list.
                    slot.ClearSlot();
                    slots.Remove(slot);
                }
                else
                {
                    // If the slot contains enough items, deduct the remaining quantity from the slot.
                    slot.quantity -= remainingItems;

                    // All items are removed successfully.
                    remainingItems = 0;
                }
            }

            // Return the number of items that couldn't be removed (if any).
            return remainingItems;
        }


        #region XML Documentation
        /// <summary>
        /// Removes all items from a specific slot in the inventory.
        /// </summary>
        /// <param name="slotNumber">The index of the inventory slot to remove items from.</param>
        /// <remarks>
        /// This method directly removes the slot at the specified index from the inventory list.
        /// </remarks>
        #endregion
        public void RemoveItemsFromSlot(int slotNumber)
        {
            slots.RemoveAt(slotNumber);
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
