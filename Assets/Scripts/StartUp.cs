using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startUp : MonoBehaviour
{
    public GameManager gameManager;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           gameManager.StartSession();
        }
        
    }
}
