using UnityEngine;

public class RaccoonWinCollision : MonoBehaviour
{
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
        Application.Quit();
    }
}
