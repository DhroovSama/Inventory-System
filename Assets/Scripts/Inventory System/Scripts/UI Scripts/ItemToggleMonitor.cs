using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Monitors the state of toggles in a ToggleGroup and handles changes in selection.
    /// </summary>
    #endregion
    public class ItemToggleMonitor : MonoBehaviour
    {
        [Tooltip("The ToggleGroup containing the toggles to monitor.")]
        public ToggleGroup toggleGroup;

        private Toggle lastSelectedToggle; // Keeps track of the last selected toggle.

        private void Start()
        {
            // Subscribe to the value change event for each toggle in the group.
            foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
            {
                toggle.onValueChanged.AddListener(isOn =>
                {
                    if (isOn) // Check if the toggle is selected.
                    {
                        HandleToggleChange(toggle); // Handle the toggle change event
                    }
                });

                if(toggle.isOn)
                {
                    // Sorts the Generated Item Buttons at the Start according to whic  Toggle is On at starting 
                    InventoryPanelManager.Instance.FilterItemsByType(toggle.GetComponent<ToggleItemType>().itemType);
                }
            }
        }

        #region XML Documentation
        /// <summary>
        /// Handles the logic when a new toggle is selected.
        /// </summary>
        /// <param name="selectedToggle">The toggle that was selected.</param>
        #endregion
        private void HandleToggleChange(Toggle selectedToggle)
        {
            // Only proceed if the newly selected toggle is different from the last one.
            if (selectedToggle != lastSelectedToggle)
            {
                lastSelectedToggle = selectedToggle; 

                ItemType selectedType = selectedToggle.GetComponent<ToggleItemType>().itemType;

                InventoryPanelManager.Instance.FilterItemsByType(selectedType);
            }
        }
    }
}
