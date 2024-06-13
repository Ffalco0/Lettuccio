using System.Net.Sockets;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // References to game objects
    public GameObject cone;
    public GameObject[] scoops;

    // Constants for movement and sensitivity
    const float maxRotation = 15f; // Maximum rotation angle for the cone
    const float sensitivity = 5f; // Sensitivity for phone tilt to cone movement
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;
    
    private Vector3 initialConePosition;
    private Quaternion initialConeRotation;
    private Vector3[] initialScoopPositions;
    private Quaternion[] initialScoopRotations;

    void Start ()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();

        // Store initial positions and rotations
        initialConePosition = cone.transform.position;
        initialConeRotation = cone.transform.rotation;
        initialScoopPositions = new Vector3[scoops.Length];
        initialScoopRotations = new Quaternion[scoops.Length];
        for (int i = 0; i < scoops.Length; i++)
        {
            initialScoopPositions[i] = scoops[i].transform.position;
            initialScoopRotations[i] = scoops[i].transform.rotation;
        }

        // Initialize the gyroscope
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
        else
        {
            Debug.LogWarning("Gyroscope not supported on this device.");
        }
        ResetMinigame(); 

    }
    void Update()
    {
        gameManagerScript.hints.text = "Eat all the IceCream!";
        // Get the phone's gyroscope rotation
        float tilt = GetTiltAmount();

        // Rotate the cone based on the tilt
        cone.transform.Rotate(0f, 0f, -tilt);

        // Check for each scoop if it's falling off the cone
        foreach (GameObject scoop in scoops)
        {
            // Check if the scoop's position is below the cone's base
            if (scoop.transform.position.y < cone.transform.position.y * 2)
            {
                gameManagerScript.WinMinigame();
                ResetMinigame();   
            }
        }
    
    }
      float GetTiltAmount()
    {
        // Default tilt to 0 if gyroscope is not available
        float tilt = 0f;

        // Calculate tilt only if gyroscope is enabled and supported
        if (SystemInfo.supportsGyroscope && Input.gyro.enabled)
        {
            // Get the phone's gyroscope rotation
            tilt = -Input.gyro.rotationRate.y * sensitivity;

            // Clamp the tilt to avoid exceeding the maximum rotation
            tilt = Mathf.Clamp(tilt, -maxRotation, maxRotation);
        }

        return tilt;
    }

     void ResetMinigame()
    {
        // Reset cone to its initial position and rotation
        cone.transform.position = initialConePosition;
        cone.transform.rotation = initialConeRotation;

        // Reset scoops to their initial positions and rotations
        for (int i = 0; i < scoops.Length; i++)
        {
            scoops[i].transform.position = initialScoopPositions[i];
            scoops[i].transform.rotation = initialScoopRotations[i];

            // Re-enable scoops if they were destroyed
            if (!scoops[i].activeSelf)
            {
                scoops[i].SetActive(true);
            }
        }
    }
}
