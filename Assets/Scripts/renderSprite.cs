using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderSprite : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite sprite1;
    public Sprite sprite2;
    public float timePerFrame = 0.5f;

    private float timeElapsed = 0.0f;
    private bool showSprite1 = true;

    void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timePerFrame) {
            timeElapsed = 0.0f;
            showSprite1 = !showSprite1;

            if (showSprite1) {
                spriteRenderer.sprite = sprite1;
            } else {
                spriteRenderer.sprite = sprite2;
            }
        }
    }
}
