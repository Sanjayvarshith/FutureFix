using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class WeatherData
{
    public CurrentWeather current;
}

[System.Serializable]
public class CurrentWeather
{
    public string[] weather_descriptions;
    public float temperature;
    public float wind_speed;
    public float precipitation;
}

public class WeatherManager : MonoBehaviour
{
    public TextMeshProUGUI weatherText; // For displaying weather info
    public GameObject weatherDialoguePanel; // Panel with the dialogue
    public TextMeshProUGUI weatherDialogueText; // Text inside the dialogue panel
    public Button continueButton; // Button that continues to the weather panel
    public GameObject weatherPanel; // The panel that shows the weather data

    private string apiKey = "0995783e5bb11fde67aa050f83ac765c";  // Replace with actual API key
    private string location = "Tirupati";    // Replace with desired location
    private string apiUrl = "https://api.weatherstack.com/current?access_key={0}&query={1}";

    private bool continueClicked = false; // Flag to check if the button was clicked

    void Start()
    {
        // Fetch weather data
        GetWeatherData();
        
        // Hide weather panel initially
        weatherPanel.SetActive(false);
        
        // Set up the Continue button to show the weather panel when clicked
        continueButton.onClick.AddListener(OnContinueClicked);
        
        // Start showing the weather dialogue after 10 seconds
        // StartWeatherDialogue();
    }

    // Changed GetWeatherData to a regular function
    public void GetWeatherData()
    {
        string url = string.Format(apiUrl, apiKey, location);

        weatherPanel.SetActive(true);

        // Create a UnityWebRequest and set it up
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Send the request asynchronously and set up a callback to handle the response
        request.SendWebRequest().completed += (asyncOp) => OnRequestCompleted(request);
    }

    // Callback function to handle when the request is completed
    void OnRequestCompleted(UnityWebRequest request)
    {
        if (request.result == UnityWebRequest.Result.Success)
        {
            ParseWeatherData(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error: " + request.error);
            weatherText.text = "Weather: Error fetching data";
        }
    }

    void ParseWeatherData(string json)
    {
        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(json);
        string description = weatherData.current.weather_descriptions.Length > 0 ? weatherData.current.weather_descriptions[0] : "Unknown";
        float temperature = weatherData.current.temperature;
        float windSpeed = weatherData.current.wind_speed;
        float precipitation = weatherData.current.precipitation;

        Debug.Log($"Temperature: {temperature}°C");
        // Update weatherText with the fetched data
        weatherText.text = $"Weather: {description}\nTemp: {temperature}°C\nWind: {windSpeed} km/h\nRain: {precipitation} mm";
    }

    // Function to start the weather dialogue display
    public void StartWeatherDialogue()
    {
        StartCoroutine(ShowWeatherDialogue());
    }

    // Coroutine to show and hide the dialogue panel at the 10-second timestamp
    IEnumerator ShowWeatherDialogue()
    {
        // Wait for 10 seconds
        yield return new WaitForSeconds(5f);

        // Show the weather dialogue panel with the message
        weatherDialoguePanel.SetActive(true);
        weatherDialogueText.text = "Feeling Sunny!!! Get weather report?";

        // Wait for 5 seconds to check if the button was clicked
        float timer = 0f;
        while (timer < 8f)
        {
            if (continueClicked)
            {
                // If the button is clicked, exit early
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // If 5 seconds passed and the button wasn't clicked, hide the panel
        weatherDialoguePanel.SetActive(false);
    }

    // Handle Continue button click
    void OnContinueClicked()
    {
        // Hide the weather dialogue panel
        weatherDialoguePanel.SetActive(false);

        // Show the weather panel (this is the main weather information panel)
        // weatherPanel.SetActive(true);

        // Set the flag to true indicating the button was clicked
        continueClicked = true;
    }
}
