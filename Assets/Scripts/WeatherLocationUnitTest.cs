using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherLocationUnitTest : MonoBehaviour
{

    // This tester will allow me to easily select a different
    // Real world location to test the weather information from
    //
    // It will also give me the option of feeding specific
    // Weather to the program to test the different 
    // Visual aspects of weather

    public enum Location {Orlando, AtlanticCity, Marquette, LosAngelos}
    public enum Weather {Clear, Thunderstorm, Rain, Drizzle, Clouds, Mist}

    [SerializeField] private RetrieveLocation retrieveLocation;

    [Header("Test with predetermined location")]
    public bool testLocation = false;
    [SerializeField] private Location location;

    [Header("Test with predetermined weather information")]
    public bool testWeather = false;
    [SerializeField] private Weather weather;

    public void SetLocation()
    {
        switch(location)
        {
            case Location.Orlando:
                retrieveLocation.lat = "28.53";
                retrieveLocation.lon = "-81.37";
                break;
            case Location.AtlanticCity:
                retrieveLocation.lat = "39.36";
                retrieveLocation.lon = "-74.42";                
                break;
            case Location.Marquette:
                retrieveLocation.lat = "46.54";
                retrieveLocation.lon = "-87.39";
                break;
        }
    }

    public void SetWeather()
    {

    }
}
