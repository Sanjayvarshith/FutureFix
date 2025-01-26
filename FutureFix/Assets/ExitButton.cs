using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Call this method when the exit button is clicked
    public void ExitGame()
    {
        // Log a message for debugging purposes (optional)
        Debug.Log("Exiting game...");

        // Exit the application
        Application.Quit();

        // Note: This will only work in a built application.
        // In the Unity Editor, it will not close the editor. 
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
