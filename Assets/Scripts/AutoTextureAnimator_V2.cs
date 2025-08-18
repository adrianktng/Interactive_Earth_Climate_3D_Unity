using UnityEngine;
using System.Collections;
using System.Linq;
using TMPro; // --- ADDED: Required for TextMeshPro components

public class AutoTextureAnimator : MonoBehaviour
{
    [Header("Animation Settings")]
    [Tooltip("The path to the folder inside a 'Resources' folder.")]
    public string folderPathInResources = "Textures/EarthAnimationFrames"; // --- MODIFIED: Default path set

    [Tooltip("The time in seconds to wait before switching to the next texture.")]
    public float secondsPerFrame = 1.0f;

    // --- ADDED: Section for Text Settings ---
    [Header("Text Settings")]
    [Tooltip("The TextMeshPro object that will display the description.")]
    public TextMeshPro floatingText;

    [Tooltip("The descriptions for each texture. Must be in the same alphabetical order as the texture files.")]
    public string[] textureDescriptions;
    // --- END of ADDED section ---

    // Private variables
    private Renderer objectRenderer;
    private Texture2D[] animationFrames;
    private Coroutine animationCoroutine;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("AutoTextureAnimator Error: No Renderer component found on this object.");
            return;
        }

        // --- ADDED: Check if the text object has been assigned ---
        if (floatingText == null)
        {
            Debug.LogError("Floating Text object has not been assigned in the Inspector!");
            return;
        }

        LoadTexturesFromResources();

        if (animationFrames != null && animationFrames.Length > 0)
        {
            // --- ADDED: Warning if text and texture counts don't match ---
            if (textureDescriptions.Length != animationFrames.Length)
            {
                Debug.LogWarning("The number of texture descriptions does not match the number of animation frames. Text may not display correctly.");
            }
            animationCoroutine = StartCoroutine(AnimateTextures());
        }
    }

    void LoadTexturesFromResources()
    {
        if (string.IsNullOrEmpty(folderPathInResources))
        {
            Debug.LogError("Folder path is not specified in the Inspector!");
            return;
        }

        animationFrames = Resources.LoadAll<Texture2D>(folderPathInResources);

        if (animationFrames.Length == 0)
        {
            Debug.LogError($"No textures found in 'Resources/{folderPathInResources}'. Please check the path.");
        }
        else
        {
            animationFrames = animationFrames.OrderBy(t => t.name).ToArray();
            Debug.Log($"Loaded {animationFrames.Length} texture frames.");
        }
    }

    IEnumerator AnimateTextures()
    {
        int currentFrameIndex = 0;
        
        while (true)
        {
            // Set the main texture of the material
            objectRenderer.material.SetTexture("_MainTex", animationFrames[currentFrameIndex]);

            // --- ADDED: Update the text description ---
            // Check if the current index is valid for the descriptions array
            if (currentFrameIndex < textureDescriptions.Length)
            {
                floatingText.text = textureDescriptions[currentFrameIndex];
            }
            // --- END of ADDED section ---

            // Move to the next frame
            currentFrameIndex = (currentFrameIndex + 1) % animationFrames.Length;

            yield return new WaitForSeconds(secondsPerFrame);
        }
    }
}