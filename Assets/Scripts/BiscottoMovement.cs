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
    //Handle Sprite
    private SpriteRenderer spriteRenderer;
    public Sprite biscuitDURO;
    public Sprite biscuitROTTO;
    public Sprite biscuitNORMALE;

    public Sprite biscuitBAGNATO;

    //GameManager Script
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        initialPosition = biscuit.position;
        spriteRenderer = biscuit.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = biscuitNORMALE;
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


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == glass)
        {
            isDeeped = true;
        }
    }


    void CheckResults()
    {
        if(gameManagerScript.GetTime() < 3f)
        {
            //ScpriteBiscotto DURO
            spriteRenderer.sprite = biscuitDURO;
        }
        else if(gameManagerScript.GetTime() > 5f)
        {
            //ScpriteBiscotto ROTTO
            spriteRenderer.sprite = biscuitROTTO;
        }
        else
        {
            spriteRenderer.sprite = biscuitBAGNATO;
        }

        isDeeped = false;
    }
}
