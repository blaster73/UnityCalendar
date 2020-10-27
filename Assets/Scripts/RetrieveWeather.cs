using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using TMPro;

public class RetrieveWeather : MonoBehaviour
{

    // Reference the script that gets user coordinates
    [SerializeField] private RetrieveLocation retrieveLocation;

    // Reference hourly weather
    [SerializeField] private RetrieveHourlyWeather retrieveHourlyWeather;

    // Reference the script that checks user location permission
    [SerializeField] private GetLocationPermission getLocationPermission;

    // Reference the weather particle script
    [SerializeField] private WeatherParticle weatherParticle;

    // Reference the Main Canvas script
    [SerializeField] private MainCanvas mainCanvas;
    [SerializeField] private GameObject intro;
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private TMP_Text debugTextTop;

    public float celcius;
    public float fahrenheit;

    // Reference Unit Tester
    [SerializeField] private WeatherLocationUnitTest weatherLocationUnitTest;

    private string baseUrl = "api.openweathermap.org/data/2.5/weather?lat=";
    private string preLon = "&lon=";
    private string preKey = "&appid=";
    private string apiKey = "547bc949f1ca09f43cfe8f9994a3bdf4";

    private string mainCondition;
    private string mainDescription;
    private string temperature;
    private string city;

    private void Start()
    {
        //GetWeather();
        debugTextTop.text = "Started Get Weather";
    }

    [ContextMenu("Get Weather")]
    public void GetWeather()
    {
        StartCoroutine(GetWeatherRoutine());
    }

    // GET weather JSON for user coordinates using web request
    IEnumerator GetWeatherRoutine()
    {
        // If location access start getting GPS location
        if (getLocationPermission.locationEnabled == true)
        {
            retrieveLocation.StartFindingLocation();
        }
        
        // Hold until weather location information is received
        int maxWait = 20;
        while (retrieveLocation.done == false && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
            Debug.Log("Waiting");
        }

        string url = baseUrl + retrieveLocation.lat + preLon + retrieveLocation.lon + preKey + apiKey;

        // Start web request
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        // If location access and actual location are available
        if(request.isDone && getLocationPermission.locationEnabled == true)
        {
            //intro.SetActive(false);
            intro.GetComponent<Animator>().SetTrigger("Fade");
        }

        Debug.Log(request.downloadHandler.text);
        debugText.text = request.downloadHandler.text;
        ParseWeatherData(request.downloadHandler.text);

        // Start getting hourly weather
        retrieveHourlyWeather.GetHourlyWeather(retrieveLocation.lat, retrieveLocation.lon);
    }

    void ParseWeatherData(string weatherData)
    {
        if(weatherLocationUnitTest.testWeather == true)
        {


            //mainCanvas.setWeatherText(mainConditionAltered, celcius.ToString("F1") + "°");
        }


        // Create regex to filter for weather and temperature
        // Pre-escaped regex input (?<=main":"|"description":"|"temp":|"name":").*?(?=,")
        var rx = new Regex(@"(?<=main"":""|""description"":""|""temp"":|""name"":"").*?(?=,"")");
        MatchCollection matches = rx.Matches(weatherData);

        mainCondition = matches[0].Value;
        mainDescription = matches[1].Value;
        temperature = matches[2].Value;
        city = matches[3].Value;
        Debug.Log(city);


        // Set the weather visual effects
        weatherParticle.SetWeather(mainCondition, mainDescription);

        // Convert to different temperture measurements
        float kelvin = float.Parse(temperature, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
        celcius = kelvin - 273.15f;
        fahrenheit = (kelvin - 273.15f) * 1.8f + 32;
        string celciusString = celcius.ToString("F1") + "°";

        // Remove extra quote from weather condition name and city
        string mainConditionAltered = mainCondition.Remove(mainCondition.Length - 1, 1);
        string cityAltered = city.Remove(city.Length - 1, 1);

        // Set the weather information for the canvas
        mainCanvas.setWeatherText(mainConditionAltered, celciusString, cityAltered);
    }
}
