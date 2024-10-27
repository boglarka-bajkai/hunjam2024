using UnityEngine;
using UnityEngine.UI;

public class InvertColor : MonoBehaviour
{
    public Material invertMaterial; // Assign this in the Inspector
    private bool isColorInverted = false;
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if the space key is pressed
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ToggleColorInversion();
        }
    }

    void ToggleColorInversion()
    {
        // Toggle color inversion on or off
        isColorInverted = !isColorInverted;
        if (isColorInverted)
        {
            mainCamera.SetReplacementShader(invertMaterial.shader, null);
        }
        else
        {
            mainCamera.ResetReplacementShader();
        }
    }
}