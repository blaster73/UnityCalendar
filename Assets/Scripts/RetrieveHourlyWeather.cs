using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text.RegularExpressions;
using TMPro;

public class RetrieveHourlyWeather : MonoBehaviour
{
    // Reference MainCanvas script
    [SerializeField] private MainCanvas mainCanvas;

    // Create Dictionary of <string Hour: List (Celcius, fahrenheit, weather)>
    public Dictionary<string, List<string>> hourInfo = new Dictionary<string, List<string>>();

    private string baseUrl = "https://api.openweathermap.org/data/2.5/onecall?lat=";
    private string preLon = "&lon=";
    private string exclude = "&exclude=current,minutely,daily,alerts";
    private string preKey = "&appid=";    
    private string apiKey = "547bc949f1ca09f43cfe8f9994a3bdf4";

    [ContextMenu("Test Hourly Weather")]
    public void TestHourlyWeather()
    {
        hourInfo = new Dictionary<string, List<string>>();
        StartCoroutine(GetWeatherRoutine("28.53", "-81.37"));
    }

    public void GetHourlyWeather(string lat, string lon)
    {
        StartCoroutine(GetWeatherRoutine(lat, lon));
    }

    // GET weather JSON for user coordinates using web request
    IEnumerator GetWeatherRoutine(string lat, string lon)
    {
        string url = baseUrl + lat + preLon + lon + exclude + preKey + apiKey;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);        
        ParseHourlyWeatherData(request.downloadHandler.text);
    }

    void ParseHourlyWeatherData(string weatherData)
    {

        // Pre escaped regex string: (?<=main":"|"temp":|{"dt":).*?(?=,")        
        var rx = new Regex(@"(?<=main"":""|""temp"":|{""dt"":).*?(?=,"")");
        MatchCollection matches = rx.Matches(weatherData);

        // Temp variables for populating dictionary
        int tempIndex = 0;
        string tempHour = "";

        // Create the lists and initialize
        List<string>[] tempDatas = new List<string>[24];
        for(int i = 0; i < tempDatas.Length; i++)
        {
            tempDatas[i] = new List<string>();
        }
            
        // Create index for the lists
        int listIndex = 0;

        // Format and place data in Dictionary
        for(int i = 0; i < 72; i++)
        {
            if(tempIndex == 0)
            {
                // Get current hour
                double unixTime = Convert.ToDouble(matches[i].ToString());
                DateTime date = UnixTimeStampToDateTime(unixTime);
                tempHour = date.Hour.ToString() + ":00";
                //Debug.Log("Hour: " + date.Hour.ToString());

                tempIndex++;
            }
            else if(tempIndex == 1)
            {
                // Convert kelvin to float
                float kelvin = float.Parse(matches[i].ToString(), System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                // Get Celcius
                float celcius = kelvin - 273.15f;
                string celciusString = celcius.ToString("F1") + "°";
                tempDatas[listIndex].Add(celciusString);
                //Debug.Log("Celcius: " + celciusString);

                // Get Fahrenheit
                float fahrenheit = (kelvin - 273.15f) * 1.8f + 32;
                string fahrenheitString = fahrenheit.ToString("F1") + "°";
                tempDatas[listIndex].Add(fahrenheitString);
                //Debug.Log("fahrenheit: " + fahrenheitString);

                tempIndex++;
            }
            else
            {
                // Format and add weather
                string weather = matches[i].ToString();
                string weatherAltered = weather.Remove(weather.Length - 1, 1);
                tempDatas[listIndex].Add(weatherAltered);
                //Debug.Log("weather: " + weatherAltered);

                // Add to the houtInfo dictionary
                hourInfo.Add(tempHour, tempDatas[listIndex]);

                tempIndex = 0;
                listIndex++;

            }
        }

        // Call the main canvas to show the hourly weather info
        mainCanvas.SetHourlyWeather(hourInfo);

    }

    // Convert Unix time to a datetime
    public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {        
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
        return dtDateTime;
    }
}
