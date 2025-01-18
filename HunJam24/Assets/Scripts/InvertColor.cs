using UnityEngine;
using UnityEngine.UI;

public class InvertColor : MonoBehaviour
{
    private static InvertColor _instance;

    void Awake()
    {
        if (_instance != null) Destroy(this);
        _instance = this;
    }

    public static InvertColor Instance => _instance;    
    public Material invertMaterial; // Assign this in the Inspector
    private bool isColorInverted = false;
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene
        mainCamera = Camera.main;
    }

    public void ToggleColorInversion()
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
    public void ResetColor() {
        isColorInverted = false;
        mainCamera.ResetReplacementShader();
    }
}