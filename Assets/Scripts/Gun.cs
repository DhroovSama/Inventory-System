using UnityEngine;
using Inventory.sys;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [Tooltip("Event triggered when the gun is fired")]
    public UnityEvent onGunShoot;  

    [SerializeField, Tooltip("Action reference for shooting the gun")]
    private InputActionReference gunShootAction;

    [Tooltip("Cooldown time between gunshots")]
    public float fireCoolDown;

    [Tooltip("Tracks the remaining cooldown time")]
    private float currentCoolDown; 

    [SerializeField, Tooltip("Reference to the inventory system")]
    private InventorySystem inventorySystem; 

    [SerializeField]
    private ItemData gunItemDataSO;  
     
    [SerializeField, Tooltip("The gun GameObject to be enabled or disabled")]
    private GameObject gunGameObject;  

    private void OnEnable()
    {
        inventorySystem.onInventoryChanged += CheckGunAvailability;

        gunShootAction.action.Enable();
    }

    private void Start()
    {
        currentCoolDown = fireCoolDown;
    }

    private void Update()
    {
        if (gunShootAction.action.triggered)
        {
            if (currentCoolDown <= 0f)
            {
                onGunShoot?.Invoke();  // Trigger the gun shoot event
                currentCoolDown = fireCoolDown;  // Reset cooldown
            }
        }

        // Decrease the cooldown timer
        currentCoolDown -= Time.deltaTime;
    }

    private void CheckGunAvailability()
    {
        // Enable or disable the gun based on whether the player has it in inventory
        if (IsGunInInventory())
        {
            gunGameObject.SetActive(true);
        }
        else
        {
            gunGameObject.SetActive(false);
        }
    }

    private bool IsGunInInventory()
    {
        // Check if the gun item is in the inventory
        return inventorySystem.HasItem(gunItemDataSO);
    }

    private void OnDisable()
    {
        gunShootAction.action.Disable();

        if (inventorySystem != null)
        {
            inventorySystem.onInventoryChanged -= CheckGunAvailability;
        }
    }
}
