using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.sys;

public class InventoryManager : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public ItemData item;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            inventorySystem.AddItem(item, 5);
        }
    }
}
