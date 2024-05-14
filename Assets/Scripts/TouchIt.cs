using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchIt : MonoBehaviour
{
        // Layer mask for objects we want to consider for touch
    public LayerMask touchInputMask;
    void Update()
    {
         // Check if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // Convert mouse position to a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Perform raycast to check if any object is touched
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, touchInputMask))
            {
                // Retrieve the exact GameObject that was hit by the raycast
                GameObject touchedObject = hit.transform.gameObject;

                // Check the name of the touched object and change the log accordingly
                if (touchedObject.name == "Nera")
                {
                    Debug.Log("You touched the object named Black");
                }
                else
                {
                    Debug.Log("You Lose");
                }
            }
        }
    }
}
