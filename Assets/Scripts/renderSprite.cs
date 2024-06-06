using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class renderSprite : MonoBehaviour
{
   public SpriteRenderer spriteRenderer;
    public List<Sprite> sprites; // List to hold all four sprites
    public float timePerSprite = 0.5f; // Time to display each sprite

    private int currentSpriteIndex = 0; // Index of the currently displayed sprite
    private float timeElapsed = 0.0f;

    void Start()
    {
        // Ensure the sprites list is not null or empty
        if (sprites == null || sprites.Count == 0)
        {
            Debug.LogError("No sprites assigned! Please assign four sprites in the Inspector.");
            return;
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timePerSprite)
        {
            timeElapsed = 0.0f;
            currentSpriteIndex++;

            // Wrap around to the first sprite if the end is reached
            if (currentSpriteIndex >= sprites.Count)
            {
                currentSpriteIndex = 0;
            }

            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
    }
}
