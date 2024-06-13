using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchIt : MonoBehaviour
{
        // Layer mask for objects we want to consider for touch
    public LayerMask touchInputMask;

    private GameObject gameManagerObject;
    private GameManager gameManagerScript;
     void Start ()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        
    }
    void Update()
    {
        gameManagerScript.hints.text = "Touch the demonic sheep";
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
                    gameManagerScript.WinMinigame();
                } 
            }
        }
    }
}
