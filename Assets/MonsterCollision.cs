using UnityEngine;

public class MonsterCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Raccoon"))
        {
            Debug.Log("Raccoon collided with Monster!");
            SceneLoader.ReloadCurrentScene(); 
        }
    }
}
