using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    public class InventorySystem : MonoBehaviour
    {
        public List<InventorySlot> slots = new List<InventorySlot>();
        public int maxSlots = 20;

        public void AddItem(ItemData itemData, int quantity)
        {
            if(IsFull()) return;
            slots.Add(new InventorySlot(itemData, quantity));
        }

        public bool IsFull()
        {
            return slots.Count >= maxSlots;
        }

        public void ClearInventory()
        {
            slots.Clear();
        }
    }
}

