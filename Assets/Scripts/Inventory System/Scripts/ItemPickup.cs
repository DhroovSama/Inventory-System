using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.sys
{
    public class ItemPickup : MonoBehaviour
    {
        public ItemData itemData;
        public int quantity = 1;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlayerInventorySystem playerInventory = other.GetComponent<PlayerInventorySystem>();


                if(playerInventory != null)
                {
                    quantity = playerInventory.PickupItem(itemData, quantity);
                    if(quantity <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
