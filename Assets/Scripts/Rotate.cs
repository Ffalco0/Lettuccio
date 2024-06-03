using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Rotation speed of the object
    public float rotationSpeed = 1.0f;

    // Check if gyroscope is available on the device
    private bool gyroSupported;

    // Initial device orientation
    private Quaternion initialRotation;

    public GameManager gameManager;

    void Start()
    {
        // Check if gyroscope is supported on the device
        gyroSupported = SystemInfo.supportsGyroscope;

        // If gyroscope is supported, initialize its orientation
        if (gyroSupported)
        {
            Input.gyro.enabled = true; // Enable the gyroscope
            initialRotation = Quaternion.identity; // Initial device orientation
        }
        else
        {
            Debug.Log("Gyroscope not supported on this device.");
        }
    }

    void Update()
    {
        // If gyroscope is supported, rotate the object based on the rotation on the Z axis
        if (gyroSupported)
        {
            // Get the gyroscope rotation
            Quaternion gyroRotation = Input.gyro.attitude * initialRotation;

            // Apply rotation only on the Z axis
            float targetAngleZ = gyroRotation.eulerAngles.z;

            // Rotate the object on the Z axis with a specified speed
            transform.rotation = Quaternion.Euler(0, 0, targetAngleZ * rotationSpeed);
        }

        if(gameManager.GetTimer() == 0)
        {
            gameManager.OnGameEnd(true);
        }
    }
}
