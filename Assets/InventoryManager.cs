using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.sys;

public class InventoryManager : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public ItemData item;
    public ItemData item1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            inventorySystem.AddItem(item, 1);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            inventorySystem.AddItem(item1, 1);
        }
    }
}
