using UnityEngine;
using TMPro; // using TextMeshPro library
 namespace getReal3D
{
public class TextureChanger : MonoBehaviour
{
    // Assign your textures (PNGs) in the Inspector
    public Texture[] albedoTextures;
    public string[] textureDescriptions;
    public TextMeshPro descriptionText;

    private int currentIndex = 0;
    private Renderer objectRenderer;

    void Start()
    {
        // Get the Renderer component from the object this script is attached to
        objectRenderer = GetComponent<Renderer>();

        // Optional: Set the initial texture on start
        if (albedoTextures.Length > 0)
        {
            objectRenderer.material.SetTexture("_MainTex", albedoTextures[0]);
        }
    }

    void Update()
    {
            // Check if the space bar is pressed down
            //if (Input.GetKeyDown(KeyCode.C))
            if (Input.GetButtonDown("WandDrive"))
            {
                // Make sure there are textures to switch to
                if (albedoTextures.Length == 0)
                {
                    Debug.LogWarning("No textures assigned to the TextureChanger script.");
                    return;
                }

                // Move to the next texture index
                currentIndex++;

                // If we've gone past the end of the array, loop back to the start
                if (currentIndex >= albedoTextures.Length)
                {
                    currentIndex = 0;
                }

                // Change the material's main texture (Albedo)
                // "_MainTex" is the standard property name for the albedo texture in Unity's Standard Shader.
                objectRenderer.material.SetTexture("_MainTex", albedoTextures[currentIndex]);
                // Update the text, if a description exists for the current index
                if (textureDescriptions.Length > currentIndex)
                {
                    descriptionText.text = textureDescriptions[currentIndex];
                }
        }
    }
}
}