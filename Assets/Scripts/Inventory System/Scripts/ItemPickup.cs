using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Inventory.sys
{
    #region XML Documentation
    /// <summary>
    /// Handles the pickup of items by the player
    /// </summary> 
    #endregion
    public class ItemPickup : MonoBehaviour
    {
        public ItemData itemData;
        public int quantity = 1;

        private void Start()
        {
            gameObject.GetComponent<Collider>().isTrigger = false; // Ensure the collider is not a trigger initially
            gameObject.GetComponent<Rigidbody>().isKinematic = false; // Allow physics interactions
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                PlayerInventorySystem playerInventory = other.GetComponent<PlayerInventorySystem>();


                if(playerInventory != null)
                {
                    // Attempt to add the item to the player's inventory.
                    quantity = playerInventory.PickupItem(itemData, quantity);


                    if (quantity <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
