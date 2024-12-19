using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    public class InventorySystem : MonoBehaviour
    {
        [Tooltip("All the inventory slots we have")]
        public List<InventorySlot> slots = new List<InventorySlot>();
        [Tooltip("How many slots we can have at most")]
        public int maxSlots = 20;                                     

        public void AddItem(ItemData itemData, int quantity)
        {
            // If we at max capacity, don't add anything
            if (IsFull()) return;

            // if not  at max capacity add a new slot with the given item and quantity
            slots.Add(new InventorySlot(itemData, quantity));
        }

        public bool IsFull()
        {
            // true if we reached the maximum number of slots
            return slots.Count >= maxSlots;
        }

        public void ClearInventory()
        {
            // Clear all items out of the inventory
            slots.Clear();
        }
    }
}
