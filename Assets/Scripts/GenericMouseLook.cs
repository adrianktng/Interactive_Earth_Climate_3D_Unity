using UnityEngine;

public class GenericMouseLook : MonoBehaviour
{
    [Header("GML Settings")]
    [Tooltip("The sensitivity of the mouse look.")]
    public float gml_mouseSensitivity = 100f;

    // Private variables to store the rotation angles
    private float gml_xRotation = 0f; // Vertical (up/down)
    private float gml_yRotation = 0f; // Horizontal (left/right)

    void Start()
    {
        // Lock the cursor to the center of the screen and make it invisible
        Cursor.lockState = CursorLockMode.Locked;

        // Store the initial rotation of the camera
        Vector3 initialEulerAngles = transform.eulerAngles;
        gml_xRotation = initialEulerAngles.x;
        gml_yRotation = initialEulerAngles.y;
    }

    void Update()
    {
        // Get mouse input values
        float mouseX = Input.GetAxis("Mouse X") * gml_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * gml_mouseSensitivity * Time.deltaTime;

        // Adjust the rotation angles based on mouse input
        gml_yRotation += mouseX; // Add mouseX for horizontal rotation
        gml_xRotation -= mouseY; // Subtract mouseY for vertical rotation

        // Clamp the vertical rotation to prevent the camera from flipping upside down
        gml_xRotation = Mathf.Clamp(gml_xRotation, -90f, 90f);

        // Apply both rotations to the camera at once
        transform.rotation = Quaternion.Euler(gml_xRotation, gml_yRotation, 0f);
    }
}