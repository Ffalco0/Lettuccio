using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameObjects; // Array of game objects to switch between
    //TIME SECTION
    private float timer;
    public Image timebarImage;
    public float gameDuration = 10f; // Game duration in seconds
    
    private int currentIndex = 0;
    private bool isGameActive = false;

    public GameObject startScreen;
    public GameObject timerObj;

    void Start()
    {
        timer = gameDuration;   
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
            UpdateActiveObject();
        }
    }

    public float GetTimer()
    {
        return timer;
    }

    public void StartSession()
    {
        startScreen.SetActive(false);
        timerObj.SetActive(true);
        isGameActive = true;
    }
    public void EndSession()
    {
        startScreen.SetActive(true);
        timerObj.SetActive(false);
        
    }

    void UpdateActiveObject()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(i == currentIndex);
        }
    }

    public void SwitchActiveObject()
    {
        currentIndex = (currentIndex + 1) % gameObjects.Length;
        UpdateActiveObject();
    }

    void UpdateTimer()
    {
        timer -= Time.deltaTime;

        float remainingTime = Mathf.Max(0.0f, timer - Time.time);
        float normalizedTime = remainingTime / timer;
        timebarImage.transform.localScale = new Vector3(normalizedTime, 1.0f, 1.0f);

        if (timer <= 0)
        {
            timer = 0;
            EndGame(false);
        }
    }
    void EndGame(bool hasWon)
    {
        isGameActive = false;

        OnGameEnd(hasWon);
    }

    public void OnGameEnd(bool hasWon)
    {
        isGameActive = false;
        Debug.Log(hasWon ? "Game Won!" : "Game Lost!");
        SwitchActiveObject();
        StartNewGame();
    }
    void StartNewGame()
    {
        isGameActive = true;
        timer = gameDuration;
        UpdateActiveObject();
        // Reimposta lo stato del gioco come necessario
    }

}

