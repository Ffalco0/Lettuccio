using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineStory : MonoBehaviour
{
    public float cutsceneDuration = 20f; 
     //Handle Sprite
    private SpriteRenderer spriteRenderer;
    public List<Sprite> vignettes;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        //StartCoroutine(CutsceneCoroutine());
    }

    IEnumerator CutsceneCoroutine()
    {
        Debug.Log("Cutscene started");
        /*
        for (int i = 0; i< 7; i++)
        {
            spriteRenderer.sprite = vignettes[i];
            yield return new WaitForSeconds(3f);

        }
        */
        
        yield return new WaitForSeconds(cutsceneDuration);
        Debug.Log("Cutscene ended");
    }
}

