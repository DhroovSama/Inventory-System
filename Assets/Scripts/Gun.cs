using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public UnityEvent onGunShoot;

    [SerializeField]
    private InputActionReference gunShootAction;

    public float fireCoolDown;

    private float currentCoolDown;

    private void OnEnable()
    {
        gunShootAction.action.Enable();
    }

    private void Start()
    {
        currentCoolDown = fireCoolDown;
    }

    private void Update()
    {
        if(gunShootAction.action.triggered)
        {
            if(currentCoolDown <= 0f)
            {
                onGunShoot?.Invoke();

                currentCoolDown = fireCoolDown;
            }
        }

        currentCoolDown -= Time.deltaTime;
    }

    private void OnDisable()
    {
        gunShootAction.action.Disable();
    }
}
