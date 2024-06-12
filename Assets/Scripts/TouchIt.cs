using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchIt : MonoBehaviour
{
        // Layer mask for objects we want to consider for touch
    public LayerMask touchInputMask;
    public GameManager gameManager;
     void Start ()
    {
        
        gameManager.hints.text = "Touch the demonic sheep";
    }
    void Update()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Convert mouse position to a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Perform raycast to check if any object is touched
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, touchInputMask))
            {
                // Retrieve the exact GameObject that was hit by the raycast
                GameObject touchedObject = hit.transform.gameObject;

                // Check the name of the touched object and change the log accordingly
                if (touchedObject.name == "BlackSheep")
                {
                    Debug.Log("You Won!!!!");
                    gameManager.WinMinigame();
                } else
                {
                    Debug.Log("You Lose");
                }
            }
        }
    }
}
