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
<<<<<<< Updated upstream

    public Sprite biscuitBAGNATO;

=======
    public Sprite biscuitBAGNATO;
>>>>>>> Stashed changes
    //GameManager Script
    public GameObject gameManagerObject;
    public GameObject gocciaPrefab;
    public GameManager gameManagerScript;

    // Timer for dipping
    private float dippingTimer = 0f;
    private bool isDipping = false;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        initialPosition = biscuit.position;
        spriteRenderer = biscuit.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = biscuitNORMALE;
<<<<<<< Updated upstream
=======
        gameManagerScript.hints.text = "Deep The Cookie!";
        gocciaSpawnPoint = GameObject.FindWithTag("GocciaSpawn");
>>>>>>> Stashed changes
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            isTouched = true;
            isReturning = false;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouched = false;
            isReturning = true;
            if(isDeeped)
            {
                CheckResults();
            }

        }

        if (isTouched && !isDeeped)
        {
            biscuit.position = Vector3.MoveTowards(biscuit.position, glass.position, speed * Time.deltaTime);
        }
        else if (isReturning)
        {
            biscuit.position = Vector3.MoveTowards(biscuit.position, initialPosition, speed * Time.deltaTime);
            if (biscuit.position == initialPosition)
            {
                isReturning = false;
            }
        }

        // Update the dipping timer
        if (isDipping)
        {
            dippingTimer += Time.deltaTime;
        }

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
<<<<<<< Updated upstream
        if(gameManagerScript.GetTime() < 3f)
=======
        isDeeped = false;

        if (dippingTimer < 3f)
>>>>>>> Stashed changes
        {
            // SpriteBiscotto DURO
            spriteRenderer.sprite = biscuitDURO;
        }
<<<<<<< Updated upstream
        else if(gameManagerScript.GetTime() > 5f)
=======
        else if (dippingTimer > 5f)
>>>>>>> Stashed changes
        {
            // SpriteBiscotto ROTTO
            spriteRenderer.sprite = biscuitROTTO;
        }
        else
        {
            spriteRenderer.sprite = biscuitBAGNATO;
<<<<<<< Updated upstream
=======
            Debug.Log("YOU WIN");
            gameManagerScript.hints.text = "YOU WIN";
            StartCoroutine(SpawnGoccia());
>>>>>>> Stashed changes
        }
    }
    IEnumerator SpawnGoccia()
    {
        while (true)
        {
            if (gocciaSpawnPoint != null)
            {
                // Use the position of the GocciaSpawn point
                Vector3 spawnPosition = new Vector3(gocciaSpawnPoint.transform.position.x, gocciaSpawnPoint.transform.position.y, gocciaSpawnPoint.transform.position.z);

                // Spawn a Goccia object at the calculated position
                Instantiate(gocciaPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second before spawning the next Goccia
        }
    }
}
