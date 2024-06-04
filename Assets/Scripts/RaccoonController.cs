using UnityEngine;
using UnityEngine.EventSystems;

public class RaccoonController : MonoBehaviour
{
    public float upwardSpeed = 1f;
    public float moveDistance = 0.1f;
    public float speed = 0.5f;
    private bool isMoving = false;

    void Update()
    {
        transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime);

        if (Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
            MoveRaccoon();
        }
    }

    public void MoveRaccoon()
    {
        if (!isMoving)
        {
            transform.Translate(Vector3.down * moveDistance);
            isMoving = true;
            Invoke("ResetMoving", 0.1f);
        }
    }

    void ResetMoving()
    {
        isMoving = false;
    }
}
