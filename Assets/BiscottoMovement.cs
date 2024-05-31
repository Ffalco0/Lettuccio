using UnityEngine;
using System.Collections;

public class BiscottoMovement : MonoBehaviour
{
    public float downSpeed = 2f;
    public float targetY = -0.72f;
    public float returnSpeed = 2f;
    private Vector3 originalPosition;
    private bool isActive = true;
    private bool isTiming = false;
    private float timer = 0f;

    private GameObject gocciaSpawnPoint;
    public GameObject biscottoRotto;
    public GameObject gocciaPrefab;
    private bool won = false;
    void Start()
     {{
     
        gocciaSpawnPoint = GameObject.FindWithTag("gocciaspawn");
        }
{
    if (gameObject.CompareTag("BiscottoBagnato") || gameObject.CompareTag("BiscottoRotto"))
    {
        originalPosition = new Vector3(transform.position.x, 3f, transform.position.z);
    }
    else
    {
        originalPosition = transform.position;
    }
}
}


    void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButton(0))
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), downSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
            }
        }
    }

    public void SetActiveState(bool state)
    {
        isActive = state;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bicchiere"))
        {
            Debug.Log($"{gameObject.name} entered Bicchiere trigger.");
            if (!isTiming && gameObject.CompareTag("BiscottoBagnato")) // Check if attached to BiscottoBagnato
            {
                StartCoroutine(TimingCoroutine());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Bicchiere"))
        {
            Debug.Log($"{gameObject.name} exited Bicchiere trigger.");
            StopCoroutine(TimingCoroutine());
            isTiming = false;
            timer = 0f;
        }
    }

    IEnumerator TimingCoroutine()
    {
        isTiming = true;
        timer = 0f;

        while (isTiming)
        {
            timer += Time.deltaTime;

            if (timer >= 3f && timer <= 4f)
            {
                // Player wins if timer is between 3 and 5 seconds
                won = true;
            }
            else if (timer > 4f)
            {
                // Player loses if timer exceeds 6 seconds
                won = false;
                break; // Exit the loop
            }

            yield return null;
        }

         if (won)
        {
            Debug.Log("YOU WIN");

            // Spawn Goccia objects each second at the GocciaSpawn position
            StartCoroutine(SpawnGoccia());
        }
        else
        {
            Debug.Log("YOU LOSE");

            // Deactivate BiscottoBagnato
            gameObject.SetActive(false);

            // Activate BiscottoRotto directly using the reference
            if (biscottoRotto != null)
            {
                biscottoRotto.SetActive(true);
            }
            else
            {
                Debug.LogError("BiscottoRotto reference is not set.");
            }
        }
    }
    IEnumerator SpawnGoccia()
    {
        while (true)
        {
            if (gocciaSpawnPoint != null)
            {
               
                Vector3 spawnPosition = new Vector3(gocciaSpawnPoint.transform.position.x, gocciaSpawnPoint.transform.position.y, gocciaSpawnPoint.transform.position.z);

            
                Instantiate(gocciaPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(1f); 
        }
    }
}

