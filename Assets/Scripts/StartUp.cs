using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startUp : MonoBehaviour
{
    public GameManager gameManager;
    private bool isFirstTime = false;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isFirstTime)
            {
                gameManager.LoadStory();
            }
            else
            {
                gameManager.StartSession();
            }
            
        }
        
    }
}
