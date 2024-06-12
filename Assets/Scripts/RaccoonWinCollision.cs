using UnityEngine;

public class RaccoonWinCollision : MonoBehaviour
{
    private GameObject gameManagerObject;
    private GameManager gameManagerScript;
    Vector3 initialPosition;
    public GameObject monsterObject;
    Vector3 initialMonsterPosition;
    void Start()
    {
        initialPosition =  gameObject.transform.position;
        initialMonsterPosition = monsterObject.transform.position;
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManager>();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("win"))
        {
            Debug.Log("YOU WIN");
            gameManagerScript.WinMinigame();
            gameObject.transform.position = initialPosition;
            monsterObject.transform.position = initialMonsterPosition;
        }
    }
}
