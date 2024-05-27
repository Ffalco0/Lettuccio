using UnityEngine;

public class BiscottoBagnatoMovement : MonoBehaviour
{
    public float upTargetY = 0.0f;
    public float returnSpeed = 2f;
    public GameObject BiscottoRotto;
    private float timer = 0f;
    private bool timerStarted = false;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            timerStarted = true;
            timer = 0f;
        }
        else
        {
            if (timerStarted)
            {
                timer += Time.deltaTime;
                if (timer > 2f)
                {
                    gameObject.SetActive(false);
                    BiscottoRotto.transform.position = transform.position;
                    BiscottoRotto.SetActive(true);
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, upTargetY, transform.position.z), returnSpeed * Time.deltaTime);
        }
    }
}
