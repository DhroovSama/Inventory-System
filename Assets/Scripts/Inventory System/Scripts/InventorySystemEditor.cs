using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Inventory.sys;
using System.Runtime.CompilerServices;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// This custom editor provides a custom GUI for the Unity Inspector window.
    /// displays inventory slots and their details in a structured manner.
    /// </summary> 
    #endregion
    [CustomEditor(typeof(InventorySystem))]
    public class InventorySystemEditor : Editor
    {
        private Color lineColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);

        private float rowHeight = 20f;

        public override void OnInspectorGUI()
        {
            // set target object as InventorySystem type
            InventorySystem inventorySystem = (InventorySystem)target;

            // Update serialized object to track changes in the inspector
            serializedObject.Update();

            // Display the maxSlots property in the Inspector
            EditorGUILayout.PropertyField(serializedObject.FindProperty("maxSlots"));

            if (inventorySystem.slots == null || inventorySystem.slots.Count == 0)
            {
                // Display a red message if the inventory is empty
                GUIStyle redStyle = new GUIStyle(EditorStyles.label);
                redStyle.normal.textColor = Color.red;

                EditorGUILayout.LabelField("Inventory is Empty", redStyle);
            }
            else
            {
                // Begin vertical layout for inventory rows
                GUILayout.BeginVertical();

                // Draw the header row and a horizontal line
                DrawRowHeader();
                DrawHorizontalLine();

                // Loop through all inventory slots and display them
                for (int i = 0; i < inventorySystem.slots.Count; i++)
                {
                    DrawRow(inventorySystem.slots[i], i);
                    DrawHorizontalLine();
                }

                // End vertical layout
                GUILayout.EndVertical();
            }

            // Apply changes made to the serialized object
            serializedObject.ApplyModifiedProperties();
        }

        #region XML Documentation
        /// <summary>
        /// Draws the header row for the inventory display.
        /// </summary> 
        #endregion
        private void DrawRowHeader()
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Item Name", EditorStyles.boldLabel, GUILayout.Width(200));
            EditorGUILayout.LabelField("Quantity", EditorStyles.boldLabel, GUILayout.Width(200));
            GUILayout.EndHorizontal();
        }

        #region XML Documentation
        /// <summary>
        /// Draws a single row in the inventory display.
        /// </summary>
        /// <param name="slot">The inventory slot to display.</param>
        /// <param name="index">The index of the slot.</param> 
        #endregion
        private void DrawRow(InventorySlot slot, int index)
        {
            GUILayout.BeginHorizontal();

            // Display the item name or emptySlot if no item is present.
            string itemName = slot.itemData != null ? slot.itemData.name : "Empty Slot";
            EditorGUILayout.LabelField(itemName, GUILayout.Width(200));

            // Display the item quantity or 0 if no item is present.
            int quantity = slot.itemData != null ? slot.quantity : 0;
            EditorGUILayout.LabelField(quantity.ToString(), GUILayout.Width(200));

            GUILayout.EndHorizontal();
        }

        #region XML Docuumentation
        /// <summary>
        /// Draws a horizontal line for visual separation between rows.
        /// </summary> 
        #endregion
        private void DrawHorizontalLine()
        {
            // Get a rectangle for the line and draw it
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            rect.height = 1;
            EditorGUI.DrawRect(rect, lineColor);
        }
    }
}
