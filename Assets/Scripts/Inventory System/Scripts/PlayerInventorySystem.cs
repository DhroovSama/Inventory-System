using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    public class PlayerInventorySystem : MonoBehaviour
    {
        public InventorySystem inventorySystem;

        public int PickupItem(ItemData itemData, int quantity)
        {
            if(!inventorySystem.IsFull() || itemData.isStackable)
            {
                return inventorySystem.AddItem(itemData, quantity);
            }
            return quantity;
        }
    }
}
