using UnityEngine;
using TMPro; // Use the TextMeshPro library

// Make sure the filename is "TextureChanger.cs"
public class TextureChanger : MonoBehaviour
{
    // --- Assign these in the Inspector ---
    public Texture[] albedoTextures;
    public string[] textureDescriptions;
    public TextMeshPro descriptionText;
    // ------------------------------------

    private int currentIndex = 0;
    private Renderer objectRenderer;

    void Start()
    {
        // Get the Renderer component from this object
        objectRenderer = GetComponent<Renderer>();

        // Check for common setup errors
        if (objectRenderer == null)
        {
            Debug.LogError("TextureChanger Error: No Renderer component found on this object.");
            return;
        }
        if (descriptionText == null)
        {
            Debug.LogError("TextureChanger Error: The 'Description Text' field has not been assigned in the Inspector.");
            return;
        }
        if (albedoTextures.Length == 0)
        {
            Debug.LogWarning("TextureChanger Warning: The 'Albedo Textures' array is empty.");
            return;
        }
        if (albedoTextures.Length != textureDescriptions.Length)
        {
            Debug.LogWarning("TextureChanger Warning: The number of textures does not match the number of descriptions.");
        }

        // Set the initial texture and text on start
        UpdateTextureAndText();
    }

    void Update()
    {
        // Check if the 'C' key is pressed down
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Move to the next texture index, looping back to the start if necessary
            currentIndex = (currentIndex + 1) % albedoTextures.Length;
            UpdateTextureAndText();
        }
    }

    void UpdateTextureAndText()
    {
        // Ensure we don't go out of bounds
        if (albedoTextures.Length == 0 || currentIndex >= albedoTextures.Length)
        {
            return;
        }

        // Change the material's main texture (Albedo)
        objectRenderer.material.SetTexture("_MainTex", albedoTextures[currentIndex]);

        // Update the text, if a description exists for the current index
        if (textureDescriptions.Length > currentIndex)
        {
            descriptionText.text = textureDescriptions[currentIndex];
        }
    }
}