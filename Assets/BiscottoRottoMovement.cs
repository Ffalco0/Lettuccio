using UnityEngine;

public class BiscottoRottoMovement : MonoBehaviour
{
    public float upTargetY = 0.0f;
    public float returnSpeed = 2f;

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, upTargetY, transform.position.z), returnSpeed * Time.deltaTime);
    }
}
