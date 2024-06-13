using Unity.VisualScripting;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;
    public float speed = 1f;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
       
    }
    void Update()
    {
         gameManagerScript.hints.text = "Run Away From The Monster!";
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Raccoon"))
        {
            if(!gameManagerScript.GetWathcedAds())
            {
                gameManagerScript.EndGame();
            }
            else
            {
                gameManagerScript.LoseGame();
            }
        }
    }
}
