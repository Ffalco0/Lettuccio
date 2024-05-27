using UnityEngine;
using UnityEngine.EventSystems;

public class RaccoonController : MonoBehaviour, IPointerClickHandler
{
    public float upwardSpeed = 1f;
    public float moveDistance = 0.1f;
    public float speed = 0.5f;
    private bool isMoving = false;

    void Update()
    {
        transform.Translate(Vector3.up * upwardSpeed * Time.deltaTime);

        if (isMoving)
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        MoveRaccoon();
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
