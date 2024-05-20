using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isSwiping = false;
    private float swipeThreshold = 100f; // Adjust as needed
    public Transform objectToMove;
    public float minMoveSpeed = 2f; // Velocità minima
    public float maxMoveSpeed = 10f; // Velocità massima

    public GameManager gameManager;
    public GameObject prefab;

    void Start()
    {
        InstantiatePrefab();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPos = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    touchEndPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        isSwiping = false;
                        float swipeDistance = (touchEndPos - touchStartPos).magnitude;

                        if (swipeDistance > swipeThreshold)
                        {
                            Vector2 swipeDirection = (touchEndPos - touchStartPos).normalized;

                            // Determine the direction of swipe
                            if (Mathf.Abs(swipeDirection.x) < Mathf.Abs(swipeDirection.y))
                            {
                                if (swipeDirection.y > 0)
                                {
                                    // Swipe up
                                    Debug.Log("Swipe up detected.");
                                    MoveObject(swipeDistance);
                                }
                            }
                        }
                    }
                    break;
            }
        }
    }

    void InstantiatePrefab()
    {
        if(prefab.IsDestroyed())
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
    }
    void MoveObject(float swipeDistance)
    {
        // Calcola la velocità di movimento in base alla distanza percorsa durante lo swipe
        float moveSpeed = Mathf.Lerp(minMoveSpeed, maxMoveSpeed, swipeDistance / Screen.height);

        // Muovi l'oggetto nella direzione dello swipe con la velocità calcolata
        Vector2 direction = Vector2.up; // Muovi verso l'alto
        objectToMove.Translate(direction * moveSpeed * Time.deltaTime);
    }


     void OnCollisionEnter(Collision collision)
    {
        // Esempio di azione da eseguire quando si verifica una collisione
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.name == "Sofa")
        {
            Debug.Log("Complete the minigame!!!!");
            Destroy(prefab);
            gameManager.OnGameEnd(true);
        }

        // Puoi eseguire qui altre azioni, ad esempio cambiare il colore dell'oggetto, 
        // attivare/deattivare un'altra funzionalità, riprodurre un suono, ecc.
    }
}



