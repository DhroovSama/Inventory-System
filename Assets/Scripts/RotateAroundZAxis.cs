using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundZAxis : MonoBehaviour
{
    // Rotation speed in degrees per second
    [SerializeField]
    private float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        // Rotate around the Z-axis (vector3.forward = (0, 0, 1))
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }
}
