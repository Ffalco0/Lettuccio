using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    private float timer = 0f;
    private bool timerStarted = false;

    // Public variables for Biscotto and BiscottoBagnato
    public GameObject Biscotto;
    public GameObject BiscottoBagnato;
    public GameObject BiscottoRotto;

    void Update()
    {
        if (Biscotto.transform.position.y <= -0.4f)
        {
            if (!timerStarted)
            {
                timerStarted = true;
                timer = 0f; 
            }

            timer += Time.deltaTime;
            if (timer >= 3f && timer <= 4f)
            {
                Debug.Log("YOU WIN");

                
                Vector3 newPosition = BiscottoBagnato.transform.position;
                newPosition.y = Biscotto.transform.position.y;
                BiscottoBagnato.transform.position = newPosition;

                
                BiscottoBagnatoMovement biscottoBagnatoMovement = BiscottoBagnato.GetComponent<BiscottoBagnatoMovement>();
                if (biscottoBagnatoMovement != null)
                {
                    
                    biscottoBagnatoMovement.upTargetY = BiscottoBagnato.transform.position.y;
                }

                
                BiscottoBagnato.SetActive(true);
                Biscotto.SetActive(false);
            }
        }
        else
        {
            timerStarted = false;
            timer = 0f; 
        }
    }
}
