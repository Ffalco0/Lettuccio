using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Apple.GameKit;
using System;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject[] minigames; // Array of game objects to switch between

    //TIME SECTION
    private float timer = 0f; // Variable that handles the time
    public Image timebarImage;
    public float gameDuration = 10f; // Game duration in seconds
    
    //Scene Manager
    private int currentIndex = 0;
    private bool isGameActive = false;
    public GameObject startScreen;
    public GameObject loseScreen;
    public GameObject magazine;
    public GameObject minigamesScreen;
    //public GameObject magazineScreen;
    public GameObject timerObj;

    //Login Variable
    string Signature;
    string TeamPlayerID;
    string Salt;
    string PublicKeyUrl;
    string Timestamp;

    //ADS Manager
    public InterstitialAd adManager;
    public GameObject adPanelRevive;

    //Points
    public TextMeshProUGUI textComponent;
    private int Points;

    public TextMeshProUGUI hints;
    public TextMeshProUGUI totalPoints;


    public Image imageBackgorund;
    public List<Sprite> background;

    private bool isActive = true;

    private bool watchedAds = false;


    public bool GetActive()
    {
        return isActive;
    }

    public void SetActivation(bool active)
    {
        isActive = active;
    }
    
    void Awake()
    {
        // Start the coroutine
        StartCoroutine(RunMinigames());
    }

    #if !UNITY_EDITOR   
        async void Start()
        {
            await Login();
        }
    #endif

    void Update()
    {
        if(timer >= gameDuration)
        {
           CheckLose();
           timer = 0;
        }
    }
    void CheckLose()
    {
         if(!watchedAds)
        {
            Debug.Log("Watch ads");
            EndGame();            
        }
        else
        {
            LoseGame();
        }

    }
    public void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            Debug.Log("User is not authenticated with Game Center.");
        }
    }
    IEnumerator RunMinigames()
    {
        while (!isGameActive)
        {
            yield return null;
        }

        Debug.Log("Running Minigames");

        while (isGameActive)
        {
            ActivateMinigame(currentIndex);
            timer = 0f; // Reset timer
            timebarImage.fillAmount = 0f; // Reset timebar
            Debug.Log($"Starting minigame {currentIndex}");

            while (timer < gameDuration)
            {
                timer += Time.deltaTime;
                timebarImage.fillAmount = timer / gameDuration;
                yield return null;
            }

            DeactivateMinigame(currentIndex);

            // Pass to the next minigame
            currentIndex = (currentIndex + 1) % minigames.Length;
            ActivateMinigame(currentIndex);
        }
    }

    void ActivateMinigame(int index)
    {
        Debug.Log($"Activating minigame {index}");
        minigames[index].SetActive(true);
    }

    void DeactivateMinigame(int index)
    {
        Debug.Log($"Deactivating minigame {index}");
        minigames[index].SetActive(false);
    }

    public async Task Login()
    {
        if (!GKLocalPlayer.Local.IsAuthenticated)
        {
            // Perform the authentication.
            var player = await GKLocalPlayer.Authenticate();
            Debug.Log($"GameKit Authentication: player {player}");

            // Grab the display name.
            var localPlayer = GKLocalPlayer.Local;
            Debug.Log($"Local Player: {localPlayer.DisplayName}");

            // Fetch the items.
            var fetchItemsResponse = await GKLocalPlayer.Local.FetchItems();

            Signature = Convert.ToBase64String(fetchItemsResponse.GetSignature());
            TeamPlayerID = localPlayer.TeamPlayerId;
            Debug.Log($"Team Player ID: {TeamPlayerID}");

            Salt = Convert.ToBase64String(fetchItemsResponse.GetSalt());
            PublicKeyUrl = fetchItemsResponse.PublicKeyUrl;
            Timestamp = fetchItemsResponse.Timestamp.ToString();

            Debug.Log($"GameKit Authentication: signature => {Signature}");
            Debug.Log($"GameKit Authentication: publickeyurl => {PublicKeyUrl}");
            Debug.Log($"GameKit Authentication: salt => {Salt}");
            Debug.Log($"GameKit Authentication: Timestamp => {Timestamp}");
        }
        else
        {
            Debug.Log("AppleGameCenter player already logged in.");
        }
    }

    public void StartSession()
    {
        isGameActive = true;
        startScreen.SetActive(false);
        magazine.SetActive(false);
        ResetPoints();
        minigamesScreen.SetActive(true);
        textComponent.text = Points.ToString();
        // Start the first minigame
        currentIndex = 0;
        ActivateMinigame(currentIndex);
    }

    public float GetTime()
    {
        return timer;
    }

    public void EndGame()
    {
        Time.timeScale = 0f; // Stop Time
        isGameActive = false;
        adPanelRevive.SetActive(true);
        watchedAds = true;
    }

    public void Revive()
    {
        adManager.ShowAd();
    }

    public void ContinuePlaying()
    {
        watchedAds = true;
        timer = 0;
        adPanelRevive.SetActive(false);
        Time.timeScale = 1f;
        isGameActive = true;
        WinMinigame();
    }

    public void LoseGame()
    {
        totalPoints.text = "Total Points: " + Points.ToString();
        ReportScore(Points);
        StopAllCoroutines();
        Time.timeScale = 1f;
        minigamesScreen.SetActive(false);
        adPanelRevive.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void WinMinigame()
    {
        DeactivateMinigame(currentIndex);
        timer = 0f; // Reset timer for the new minigame
        timebarImage.fillAmount = 0f; // Reset timebar for the new minigame
        
        AddPoints(60);
        // Pass to the next minigame
        currentIndex = (currentIndex + 1) % minigames.Length;

        imageBackgorund.sprite = background[UnityEngine.Random.Range(0,minigames.Length)];
        
        ActivateMinigame(currentIndex);
    }
    private void AddPoints(int pointsToAdd)
    {
        Points += pointsToAdd;
        textComponent.text = Points.ToString();
    }
    private void ResetPoints()
    {
        Points = 0;
        textComponent.text = Points.ToString();
    }
    public string leaderboardID = "12BedDuccio";
    public void ReportScore(long score)
    {
        if (Social.localUser.authenticated)
        {
            Social.ReportScore(score, leaderboardID, success =>
            {
                if (success)
                {
                    Debug.Log("Score reported successfully.");
                }
                else
                {
                    Debug.Log("Failed to report score.");
                }
            });
        }
        else
        {
            Debug.Log("User is not authenticated. Cannot report score.");
        }
    }

    public void SkipVideo()
    {
        StartSession();
    }
    public bool GetWathcedAds()
    {
        return watchedAds;
    }
}
