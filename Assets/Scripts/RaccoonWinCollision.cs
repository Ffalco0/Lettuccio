using UnityEngine;

public class RaccoonWinCollision : MonoBehaviour
{
    public GameObject gameManager;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("win"))
        {
            Debug.Log("YOU WIN");
            EndGame();
        }
    }

    private void EndGame()
    {
        //gameManager.WinMinigame();
    }
}
