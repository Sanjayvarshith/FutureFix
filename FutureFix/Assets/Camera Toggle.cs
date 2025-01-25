using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public Camera miniMapCamera; // Assign your minimap camera
    public Camera fullScreenMapCamera; // Assign your fullscreen map camera

    void Start()
    {
        // Ensure only the minimap camera is active at the start
        miniMapCamera.gameObject.SetActive(true);
        fullScreenMapCamera.gameObject.SetActive(false);
    }

    public void ToggleMap()
    {
        if (miniMapCamera.gameObject.activeSelf)
        {
            // Switch to fullscreen map
            miniMapCamera.gameObject.SetActive(false);
            fullScreenMapCamera.gameObject.SetActive(true);
        }
        else
        {
            // Switch back to minimap
            fullScreenMapCamera.gameObject.SetActive(false);
            miniMapCamera.gameObject.SetActive(true);
        }
    }
}
