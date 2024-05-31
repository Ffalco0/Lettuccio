using UnityEngine;

public class CollisionTracker : MonoBehaviour
{
    public GameObject Biscotto;
    public GameObject BiscottoBagnato;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision detected with: " + other.gameObject.name);

        if (other.gameObject == Biscotto)
        {
            Debug.Log("Biscotto collided with Bicchiere");
            SwapBiscottos();
        }
    }

    void SwapBiscottos()
    {
        // Ensure Biscotto is deactivated and BiscottoBagnato is activated correctly
        Biscotto.GetComponent<BiscottoMovement>().SetActiveState(false);
        BiscottoBagnato.transform.position = Biscotto.transform.position;
        BiscottoBagnato.SetActive(true);
        BiscottoBagnato.GetComponent<BiscottoMovement>().SetActiveState(true);
        Biscotto.SetActive(false);

        Debug.Log("Swapped Biscotto with BiscottoBagnato");
    }
}
