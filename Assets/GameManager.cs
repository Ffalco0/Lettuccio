using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] gameObjects; // Array of game objects to switch between
    public TextMeshProUGUI timerText; // UI Text element to display the timer
    public float gameDuration = 10f; // Game duration in seconds

    private int currentIndex = 0;
    private float timer;
    private bool isGameActive = true;

    void Start()
    {
        timer = gameDuration;
        UpdateActiveObject();
    }

    void Update()
    {
        if (isGameActive)
        {
            UpdateTimer();
        }
    }

    public float GetTimer()
    {
        return timer;
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
        if (timer <= 0)
        {
            timer = 0;
            EndGame(false);
        }

        timerText.text = Mathf.Ceil(timer).ToString();
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

