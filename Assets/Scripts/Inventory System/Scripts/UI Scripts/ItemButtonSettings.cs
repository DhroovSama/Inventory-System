using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Configures the display settings for an inventory slot button in the UI.
    /// Handles the item's icon, quantity, and type representation.
    /// </summary>
    #endregion
    public class ItemButtonSettings : MonoBehaviour
    {
        [SerializeField, Tooltip("The UI Image component used to display the item's icon.")]
        private Image spriteImage; 

        [SerializeField, Tooltip("The UI Text component showing the number of items in the slot.")]
        private TMPro.TextMeshProUGUI numberInSlot;

        [Tooltip("The type of item represented in this slot.")]
        private ItemType itemType; 

        #region XML Documentation
        /// <summary>
        /// Initializes the inventory slot button with the given item data and count.
        /// </summary>
        /// <param name="itemData">The data of the item to display in this slot.</param>
        /// <param name="itemCount">The number of items in the slot.</param>
        #endregion
        public void Init(ItemData itemData, int itemCount)
        {
            spriteImage.sprite = itemData.icon;

            numberInSlot.text = itemCount.ToString();

            itemType = itemData.itemType;
        }

        #region XML Documentation
        /// <summary>
        /// Updates the quantity display text on the item's UI button.
        /// Ensures that the quantity is accurately represented in the UI.
        /// </summary>
        /// <param name="quantity">The current quantity to display.</param> 
        #endregion
        public void UpdateQuantityDisplay(int quantity)
        {
            numberInSlot.text = quantity.ToString();
        }

    }
}
