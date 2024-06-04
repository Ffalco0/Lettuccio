using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }
}
