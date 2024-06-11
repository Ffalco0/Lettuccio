using Unity.VisualScripting;
using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    public GameObject gameManagerObject;
    public GameManager gameManagerScript;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
        gameManagerScript.hints.text = "Run Away From The Monster!";
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Raccoon"))
        {
            Debug.Log("Raccoon collided with Monster!");
            gameManagerScript.EndGame();
        }
    }
}
