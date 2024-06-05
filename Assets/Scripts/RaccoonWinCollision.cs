using UnityEngine;

public class RaccoonWinCollision : MonoBehaviour
{
    private GameObject gameManagerObject;
    private GameManager gameManagerScript;

    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("win"))
        {
            Debug.Log("YOU WIN");
            gameManagerScript.WinMinigame();
        }
    }
}
