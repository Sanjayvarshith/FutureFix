using UnityEngine;

public class WeatherDialoguePanel : MonoBehaviour
{
    public GameObject weatherDialoguePanel;  // Reference to the panel

    // Function to show the dialogue panel
    public void ShowDialogue()
    {
        weatherDialoguePanel.SetActive(true);
    }

    // Function to hide the dialogue panel
    public void HideDialogue()
    {
        weatherDialoguePanel.SetActive(false);
    }
}
