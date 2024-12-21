using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Manages the inventory panel UI, dynamically creating item buttons, 
    /// mapping items to their types, and filtering items by type.
    /// </summary>
    #endregion
    public class InventoryPanelManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Prefab for item buttons in the inventory.")]
        private GameObject itemButtonPrefab;

        [SerializeField, Tooltip("Parent container for item buttons.")]
        private GameObject itemContainer;

        // Maps item buttons to their respective item types.
        private Dictionary<GameObject, ItemType> inventoryItemMap = new Dictionary<GameObject, ItemType>();

        #region Singleton Pattern
        public static InventoryPanelManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject); // Ensures only one instance exists.
            }
            else
            {
                Instance = this;
            }
        }
        #endregion

        [Tooltip("Temporary data for test items.")]
        public ItemData tempData1;
        public ItemData tempData2;

        private void Start()
        {
            // testing dynamically
            for (int i = 0; i < 10; i++)
            {
                GameObject itemButton = Instantiate(itemButtonPrefab, itemContainer.transform);

                // Assign random test data to the item.
                ItemData thisItemData = Random.Range(0, 2) == 1 ? tempData1 : tempData2;
                itemButton.GetComponent<ItemButtonSettings>().Init(thisItemData, Random.Range(1, 11));

                // Map the button to its item's type.
                inventoryItemMap.Add(itemButton, thisItemData.itemType);
            }
        }

        #region XML Documentation
        /// <summary>
        /// Filters displayed inventory items based on their type.
        /// </summary>
        /// <param name="type">The type of items to display.</param>
        #endregion
        public void FilterItemsByType(ItemType type)
        {
            foreach (var kvp in inventoryItemMap)
            {
                GameObject itemButton = kvp.Key;
                ItemType itemType = kvp.Value;

                // Show or hide item buttons based on the filter.
                itemButton.gameObject.SetActive(itemType == type);
            }
        }
    }
}
