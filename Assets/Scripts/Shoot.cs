using Inventory.sys;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField, Tooltip("The bullet prefab to instantiate")]
    private GameObject bulletPrefab;  

    [SerializeField, Tooltip("The point from where the bullet will be shot")]
    private Transform shootPoint;     

    [SerializeField, Tooltip("The force applied to the bullet")]
    private float bulletForce;      

    [SerializeField]
    private ItemData bulletItemDataSO, gunItemDataSO;  

    [Header("Inventory Reference")]
    [SerializeField] private InventorySystem inventorySystem; 

    public void ShootBullet()
    {
        // Check if player has both bullets and the gun in inventory
        if (inventorySystem.HasItem(bulletItemDataSO) && inventorySystem.HasItem(gunItemDataSO))
        {
            // Remove one bullet from inventory
            int bulletsToRemove = 1;

            int remainingBullets = inventorySystem.RemoveItem(bulletItemDataSO, bulletsToRemove);

            // If there are no remaining bullets, instantiate and shoot the bullet
            if (remainingBullets == 0)
            {
                InstantiateBullet();
            }
            else
            {
                Debug.Log("Not enough Bullets");
            }
        }
        else
        {
            Debug.Log("Not enough Bullets");
            return;
        }
    }

    private void InstantiateBullet()
    {
        // Instantiate the bullet prefab at the shoot point with the correct rotation
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            bulletRb.AddForce(shootPoint.forward * bulletForce, ForceMode.VelocityChange);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody attached!");
        }
    }

    public void TakeBulletFromPlayerInventory()
    {
        // Remove one bullet from inventory, if available
        if (inventorySystem != null)
        {
            inventorySystem.RemoveItem(bulletItemDataSO, 1, true);
        }
    }
}
