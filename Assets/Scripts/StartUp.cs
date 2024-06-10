using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startUp : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject magazine;
    private bool isFirstTime = true;

    void Start()
    {
       isFirstTime = PlayerPrefs.GetInt("isFirstTime", 0) == 1 ? false : true;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isFirstTime)
            {
                gameObject.SetActive(false);
                magazine.SetActive(true);
                PlayerPrefs.SetInt("isFirstTime", isFirstTime ? 1 : 0);
                PlayerPrefs.Save();
            }
            else
            {
                gameManager.StartSession();        
            }
        }
    }
}
