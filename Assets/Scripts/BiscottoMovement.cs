using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BiscottoMovement : MonoBehaviour
{
   public Transform biscuit; 
    public Transform glass;   
    public float speed = 2.0f;
    private Vector3 initialPosition;
    private bool isReturning = false;
    private bool isTouched = false;
    private bool isDeeped = false;
    public GameObject gocciaSpawnPoint;
    //Handle Sprite
    private SpriteRenderer spriteRenderer;
    public Sprite biscuitDURO;
    public Sprite biscuitROTTO;
    public Sprite biscuitNORMALE;
    public Sprite biscuitBAGNATO;
    //GameManager Script
    public GameObject gameManagerObject;
    public GameObject gocciaPrefab;
    public GameManager gameManagerScript;

    // Timer for dipping
    private float dippingTimer = 0f;
    private bool isDipping = false;

    private bool win = false;

    void Awake()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        spriteRenderer = biscuit.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = biscuitNORMALE;
        win = false;
    }
    void Start()
    {
        initialPosition = biscuit.position;
    }

    void Update()
    {
        gameManagerScript.hints.text = "Dip The Cookie!";
        // Check for mouse inputs and update states
        HandleMouseInput();

        // Handle biscuit movement
        HandleBiscuitMovement();

        // Update the dipping timer if active
        if (gameManagerScript.GetActive())
        {
            dippingTimer += Time.deltaTime;
        }

    }

    void HandleMouseInput()
    {
        if (gameManagerScript.GetActive())
        {
            if (Input.GetMouseButtonDown(0))
            {
                isTouched = true;
                isReturning = false;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isTouched = false;
                isReturning = true;

                if (isDeeped)
                {
                    CheckResults();
                }
            }
        }
    }

    void HandleBiscuitMovement()
    {
        if (isTouched && !isDeeped)
        {
            MoveBiscuitTowards(glass.position);
        }
        else if (isReturning)
        {
            MoveBiscuitTowards(initialPosition);

            // Stop returning once it reaches the initial position
            if (biscuit.position == initialPosition)
            {
                isReturning = false;
            }
        }
    }

    void MoveBiscuitTowards(Vector3 targetPosition)
    {
        biscuit.position = Vector3.MoveTowards(biscuit.position, targetPosition, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == glass)
        {
            isDeeped = true;
            isDipping = true;
            dippingTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == glass)
        {
            isDipping = false; // Stop the timer
        }
    }


    void CheckResults()
    {
        gameManagerScript.SetActivation(false);
        isDeeped = false;
        if (dippingTimer < 1.5f)
        {
            // SpriteBiscotto DURO
            spriteRenderer.sprite = biscuitDURO;
            win = false;
            StartCoroutine(ReturnAndEnd());
        }
        else if (dippingTimer > 4f)
        {
            // SpriteBiscotto ROTTO
            spriteRenderer.sprite = biscuitROTTO;
            win = false;
            StartCoroutine(ReturnAndEnd());
        }
        else
        {
            spriteRenderer.sprite = biscuitBAGNATO;
            Debug.Log("YOU WIN");
            StartCoroutine(SpawnGoccia());
            win = true;
            StartCoroutine(ReturnAndEnd());
        }
    }
   IEnumerator SpawnGoccia()
    {
        int spawnCount = 0; // Add a counter to limit the number of spawns
        while (spawnCount < 4) // Adjust this condition based on your game design
        {
            if (gocciaSpawnPoint != null)
            {
                Vector3 spawnPosition = gocciaSpawnPoint.transform.position;
                Instantiate(gocciaPrefab, spawnPosition, Quaternion.identity);
            }
            spawnCount++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator ReturnAndEnd()
    {
        yield return new WaitForSeconds(2.5f); // This is fine as it waits once
        if (win)
        {
            gameManagerScript.SetActivation(true);
            gameManagerScript.WinMinigame();
        }
        else
        {
            gameManagerScript.SetActivation(true);
            if(!gameManagerScript.GetWathcedAds())
            {
                gameManagerScript.EndGame();
            }
            else
            {
                gameManagerScript.LoseGame();
            }
            
        }
         spriteRenderer.sprite = biscuitNORMALE;
    }

}
