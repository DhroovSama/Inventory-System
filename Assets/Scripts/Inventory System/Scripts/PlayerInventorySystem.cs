using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory.sys
{
    public class PlayerInventorySystem : MonoBehaviour
    {
        public InventorySystem inventorySystem;
        public InputActionReference dropAction;
        public ItemData itemToDrop;

        private void OnEnable()
        {
            dropAction.action.Enable();
        }

        private void Update()
        {
            //It's the E key for drop action for now
            if(dropAction.action.triggered)
            {
                DropItem(itemToDrop, 6);  
            }
        }

        public int PickupItem(ItemData itemData, int quantity)
        {
            if(!inventorySystem.IsFull() || itemData.isStackable)
            {
                return inventorySystem.AddItem(itemData, quantity);
            }
            return quantity;
        }

        public void DropItemsFromSlot(int slotNumber)
        {
            inventorySystem.RemoveItemsFromSlot(slotNumber);
        }

        public void DropItem(ItemData itemData, int quantity)
        {
            inventorySystem.RemoveItem(itemData, quantity);
        }

        private void OnDisable()
        {
            dropAction.action.Disable();
        }
    }
}
