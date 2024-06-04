using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Apple.GameKit;
using System;

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
    public GameObject minigamesScreen;
    public GameObject timerObj;

    //Login Variable
    string Signature;
    string TeamPlayerID;
    string Salt;
    string PublicKeyUrl;
    string Timestamp;

    void Awake()
    {
        // Start the coroutine
        StartCoroutine(RunMinigames());
    }

    /*
    async void Start()
    {
        await Login();
    }
    */

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
                //Debug.Log($"Timer: {timer}, Fill Amount: {timebarImage.fillAmount}");
                yield return null;
            }
            
            Debug.Log("Time's up!");
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
        minigamesScreen.SetActive(true);
        
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
        isGameActive = false;
        minigamesScreen.SetActive(false);
        loseScreen.SetActive(true);
    }

    public void WinMinigame()
    {
        DeactivateMinigame(currentIndex);

        // Pass to the next minigame
        currentIndex = (currentIndex + 1) % minigames.Length;
        
        ActivateMinigame(currentIndex);
        timer = 0f; // Reset timer for the new minigame
        timebarImage.fillAmount = 0f; // Reset timebar for the new minigame
    }

    void UpdateActiveObject()
    {
        for (int i = 0; i < minigames.Length; i++)
        {
            minigames[i].SetActive(i == currentIndex);
        }
    }

    public void SwitchActiveObject()
    {
        currentIndex = (currentIndex + 1) % minigames.Length;
        UpdateActiveObject();
    }

    enum GameState
    {
        Menu,
        Game,
        Lose
    }
}
