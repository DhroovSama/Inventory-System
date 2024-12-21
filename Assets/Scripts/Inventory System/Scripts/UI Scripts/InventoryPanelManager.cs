using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        private List<ItemData> allItems;

        // Maps item buttons to their respective item types.
        private Dictionary<GameObject, ItemType> inventoryItemMap = new Dictionary<GameObject, ItemType>();

        // Maps item IDs to their preview GameObjects.
        public Dictionary<int, GameObject> previewItemObjects;


        //Singleton Pattern
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

            // Initialize the previewItemObjects dictionary and populate it with item previews.
            previewItemObjects = new Dictionary<int, GameObject>();
            foreach (var item in allItems)
            {
                GameObject obj = GameObject.Find("Preview_" + item.itemID); // Locate the preview object for each item.
                if (obj != null)
                {
                    previewItemObjects[item.itemID] = obj; // Add to the dictionary.
                    obj.SetActive(false); // Hide the preview initially.
                }
                else
                {
                    Debug.Log("Couldn't Find obj with name [Preview_]");
                }
            }

        }


        private void Start()
        {
            //// testing dynamically
            //for (int i = 0; i < 10; i++)
            //{
            //    GameObject itemButton = Instantiate(itemButtonPrefab, itemContainer.transform);

            //    // Assign random test data to the item.
            //    ItemData thisItemData = Random.Range(0, 2) == 1 ? allItems[0] : allItems[2];
            //    itemButton.GetComponent<ItemButtonSettings>().Init(thisItemData, Random.Range(1, 11));

            //    // Map the button to its item's type.
            //    inventoryItemMap.Add(itemButton, thisItemData.itemType);

            //    itemButton.GetComponent<Button>().onClick.AddListener(() => ShowItem(thisItemData.itemID));
            //}
        }

        #region XML Documentation
        /// <summary>
        /// Dynamically creates an inventory button for a given item.
        /// </summary>
        /// <param name="itemData">The data for the item to create a button for.</param>
        /// <returns>Returns the <see cref="ItemButtonSettings"/> for the created button.</returns>
        #endregion
        public ItemButtonSettings CreateInventoryButton(ItemData itemData)
        {
            GameObject itemButton = Instantiate(itemButtonPrefab, itemContainer.transform); // Create a button.
            ItemButtonSettings itemButtonSettings = itemButton.GetComponent<ItemButtonSettings>();
            itemButtonSettings.Init(itemData, 1); // Initialize the button with item data.
            inventoryItemMap.Add(itemButton, itemData.itemType); // Map the button to its item type.
            itemButton.GetComponent<Button>().onClick.AddListener(() => ShowItem(itemData.itemID)); // Add a click listener to show the item preview.
            return itemButtonSettings;
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

        #region XML Documentation
        /// <summary>
        /// Displays the preview of an item based on its item ID.
        /// Hides all other item previews.
        /// </summary>
        /// <param name="itemID">The ID of the item to display.</param>
        #endregion
        public void ShowItem(int itemID)
        {
            // Deactivate all item previews.
            foreach (var obj in previewItemObjects.Values)
            {
                obj.SetActive(false);
            }

            // Activate the preview for the selected item, if it exists.
            if (previewItemObjects.TryGetValue(itemID, out GameObject itemToShow))
            {
                itemToShow.SetActive(true);
            }
        }
    }
}
