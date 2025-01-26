using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

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
    public TextMeshProUGUI weatherText; // Assign in Inspector
    private string apiKey = "951f8578925e987695d3c37358a4401f";  // Replace with actual API key
    private string location = "Tirupati";    // Replace with desired location
    private string apiUrl = "http://api.weatherstack.com/current?access_key={0}&query={1}";

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        string url = string.Format(apiUrl, apiKey, location);
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

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
    }

    void ParseWeatherData(string json)
    {
        WeatherData weatherData = JsonUtility.FromJson<WeatherData>(json);
        string description = weatherData.current.weather_descriptions.Length > 0 ? weatherData.current.weather_descriptions[0] : "Unknown";
        float temperature = weatherData.current.temperature;
        float windSpeed = weatherData.current.wind_speed;
        float precipitation = weatherData.current.precipitation;

        // Update UI
        weatherText.text = $"Weather: {description}\nTemp: {temperature}°C\nWind: {windSpeed} km/h\nRain: {precipitation} mm";
    }
}
