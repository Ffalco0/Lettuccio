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
    
    void Start ()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        gameManagerScript.hints.text = "Balance the icecream";
    }
    void Update()
    {
        // Get the phone's gyroscope rotation
        float tilt = -Input.gyro.rotationRate.y * sensitivity;

        // Limit the tilt to avoid exceeding the maximum rotation
        tilt = Mathf.Clamp(tilt, -maxRotation, maxRotation);

        // Rotate the cone based on the tilt
        cone.transform.Rotate(0f, 0f, -tilt);

        // Check for each scoop if it's falling off the cone
        foreach (GameObject scoop in scoops)
        {
            // Check if the scoop's position is below the cone's base
            if (scoop.transform.position.y < cone.transform.position.y)
            {
                // Game Over if a scoop falls
                Debug.Log("Game Over! Scoop fell off.");
                // Handle game over logic here (e.g., reset scene, display score, etc.)
            }
        }
    }
}
