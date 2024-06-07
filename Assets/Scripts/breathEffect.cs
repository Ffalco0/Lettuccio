using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class breathEffect : MonoBehaviour
{
    // Public variables to control the breathing effect
    public TextMeshProUGUI textComponent;  // The TextMeshProUGUI component to apply the effect to
    public float scaleAmplitude = 0.1f;  // How much to scale up and down
    public float scaleFrequency = 1f;  // How fast to breathe (cycles per second)

    // Private variables to manage the effect
    private Vector3 initialScale;

    void Start()
    {
        // Capture the initial scale of the text
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }
        
        if (textComponent != null)
        {
            initialScale = textComponent.transform.localScale;
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component not found!");
        }
    }

    void Update()
    {
        // Apply the breathing effect
        if (textComponent != null)
        {
            float scale = 1 + Mathf.Sin(Time.time * scaleFrequency) * scaleAmplitude;
            textComponent.transform.localScale = initialScale * scale;
        }
    }
}
