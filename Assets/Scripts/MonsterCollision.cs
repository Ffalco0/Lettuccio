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
        gameManagerScript.hints.text = "Run Away From The Monster!";
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Raccoon") || gameManagerScript.GetTime() == 0)
        {
            Debug.Log("Raccoon collided with Monster!");
            gameManagerScript.EndGame();
        }
    }
}
