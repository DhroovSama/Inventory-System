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

        [Tooltip("Input action for Opening and Closing the Inventory.")]
        public InputActionReference inventoryOpenAction;

        [Tooltip("The item the player intends to drop.")]
        public ItemData itemToDrop;

        private void OnEnable()
        {
            dropAction.action.Enable();
            inventoryOpenAction.action.Enable();
        }

        private void Start()
        {
            InventoryPanelManager.Instance.SetPanelVisibility(false);

            inventorySystem.playerInventorySystem = this;
        }

        private void Update()
        {
            //// Check if the drop action E is pressed (temporary)
            //if (dropAction.action.triggered)
            //{
            //    DropItem(itemToDrop, 1);
            //}

            if (inventoryOpenAction.action.triggered)
            {
                OpenCloseInventory();
            }
        }

        #region XML Documentation
        /// <summary>
        /// Opens or closes the inventory panel based on its current state.
        /// </summary>
        #endregion
        public void OpenCloseInventory()
        {
            InventoryPanelManager.Instance.TogglePanelVisibility();
        }

        #region XML Documentation
        /// <summary>
        /// Picks up an item and adds it to the inventory if there's space and it's not a duplicate weapon.
        /// </summary>
        /// <param name="itemData">The item to pick up.</param>
        /// <param name="quantity">The number of items to pick up.</param>
        /// <returns>The quantity of items that could not be picked up.</returns> 
        #endregion
        public int PickupItem(ItemData itemData, int quantity)
        {
            // Check if the item is a weapon
            if (itemData.itemType == ItemType.Weapon)
            {
                // Check if the same weapon (itemID) already exists in the inventory
                foreach (var slot in inventorySystem.GetAllSlots())
                {
                    if (slot.itemData != null &&
                        slot.itemData.itemType == ItemType.Weapon &&
                        slot.itemData.itemID == itemData.itemID)
                    {
                        Debug.LogWarning("This weapon is already in the inventory!");
                        return quantity; // Return the full quantity as none can be picked up
                    }
                }
            }

            // Add the item to the inventory if there's space or it's stackable
            if (!inventorySystem.IsFull() || itemData.isStackable)
            {
                return inventorySystem.AddItem(itemData, quantity);
            }

            // Return the quantity that couldn't be picked up
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
        public Vector3 GetDropPosition()
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
            inventoryOpenAction.action.Disable(); 
        }
    }
}
