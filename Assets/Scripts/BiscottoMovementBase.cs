using UnityEngine;

public class BiscottoMovementBase : MonoBehaviour
{
    /*
    public float downSpeed = 2f;
    public float targetY = -0.72f;
    public bool isMoving = false;
    public float returnSpeed = 2f;
    public Vector3 originalPosition;

    void Start()
    {
        // For BiscottoBagnato and BiscottoRotto, set the original position with Y coordinate at 3
        if (gameObject.CompareTag("BiscottoBagnato") || gameObject.CompareTag("BiscottoRotto"))
        {
            originalPosition = new Vector3(transform.position.x, 3f, transform.position.z);
        }
        else
        {
            originalPosition = transform.position;
        }
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetY, transform.position.z), downSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, originalPosition, returnSpeed * Time.deltaTime);
        }
    }

    public void OnButtonPressed()
    {
        Debug.Log("Button2 pressed");
        isMoving = true;
    }
    */
}
