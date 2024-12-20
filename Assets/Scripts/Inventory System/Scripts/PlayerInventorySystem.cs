using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Handles the player's inventory interactions, such as picking up, dropping items, 
    /// and linking the inventory system to player actions.
    /// </summary> 
    #endregion
    public class PlayerInventorySystem : MonoBehaviour
    {
        [Tooltip("Reference to the player's inventory system.")]
        public InventorySystem inventorySystem;

        [Tooltip("Input action for dropping items.")]
        public InputActionReference dropAction;

        [Tooltip("The item the player intends to drop.")]
        public ItemData itemToDrop;

        private void OnEnable()
        {
            dropAction.action.Enable();
        }

        private void Update()
        {
            // Check if the drop action E is pressed (temporary)
            if (dropAction.action.triggered)
            {
                DropItem(itemToDrop, 1);
            }
        }

        #region XML Documentation
        /// <summary>
        /// Picks up an item and adds it to the inventory if there is space.
        /// </summary>
        /// <param name="itemData">The item to pick up.</param>
        /// <param name="quantity">The number of items to pick up.</param>
        /// <returns>The quantity of items that could not be picked up.</returns> 
        #endregion
        public int PickupItem(ItemData itemData, int quantity)
        {
            // Add the item to the inventory if theres space or its stackable.
            if (!inventorySystem.IsFull() || itemData.isStackable)
            {
                return inventorySystem.AddItem(itemData, quantity);
            }
            // Return the quantity that couldn't be picked up.
            return quantity;
        }

        #region XML Documentation
        /// <summary>
        /// Drops all items from a specific inventory slot.
        /// </summary>
        /// <param name="slotNumber">The index of the slot to drop items from.</param> 
        #endregion
        public void DropItemsFromSlot(int slotNumber)
        {
            inventorySystem.RemoveItemsFromSlot(slotNumber);
        }

        #region XML Documentation
        /// <summary>
        /// Drops a specified quantity of an item from the inventory.
        /// </summary>
        /// <param name="itemData">The item to drop.</param>
        /// <param name="quantity">The quantity of the item to drop.</param> 
        #endregion
        public void DropItem(ItemData itemData, int quantity)
        {
            // Remove the item from the inventory and calculate how many were successfully dropped.
            int couldntBeDropped = inventorySystem.RemoveItem(itemData, quantity);
            int numberDropped = quantity - couldntBeDropped;

            // If any items were dropped, instantiate the item's prefab in the game world.
            if (numberDropped > 0)
            {
                if (itemData.groupedPrefab)
                {
                    // Instantiate a single prefab with the total quantity for grouped items.
                    Instantiate(itemData.prefab, GetDropPosition(), Quaternion.identity)
                        .GetComponent<ItemPickup>().quantity = numberDropped;
                }
                else
                {
                    // Drop items individually if they are not grouped
                    Vector3 location = GetDropPosition();

                    for (int i = 0; i < numberDropped; i++)
                    {
                        // Instantiate each item 
                        Instantiate(itemData.prefab, GetDropPosition(), Quaternion.identity);
                    }
                }
            }

        }

        #region XML Documentation
        /// <summary>
        /// Calculates the position where the dropped item will appear.
        /// </summary>
        /// <returns>A vector representing the drop position.</returns> 
        #endregion
        private Vector3 GetDropPosition()
        {
            Vector3 playerPosition = transform.position; // Player's current position.
            Vector3 forwardDirection = transform.forward; // Player's forward-facing direction.
            Vector3 upDirection = transform.up; // Player's upward direction.

            float dropDistanceFromThePlayer = 2f; 

            // Calculate the drop position 
            return playerPosition + forwardDirection * dropDistanceFromThePlayer + upDirection * dropDistanceFromThePlayer;
        }

        private void OnDisable()
        {
            dropAction.action.Disable();
        }
    }
}
