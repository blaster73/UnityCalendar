using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainCanvas : MonoBehaviour
{    

    [Header("Canvas")]
    [SerializeField] private TMP_Text weatherTypeText;
    [SerializeField] private TMP_Text temperatureText;
    [SerializeField] private TMP_Text cityText;

    [Header("Hourly")]
    [SerializeField] private TMP_Text[] hours;
    [SerializeField] private TMP_Text[] weathers;
    private string[] fTemps;
    private string[] cTemps;
    [SerializeField] private TMP_Text[] temps;

    [Header("Options")]
    [SerializeField] private RetrieveWeather retrieveWeather;
    [SerializeField] private GameObject optionsContent;
    [SerializeField] private TMP_Text hourlyText;
    [SerializeField] private TMP_Text optionsText;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color deSelectedColor;
    [SerializeField] private Animator soundsAnimator;
    private bool soundsActivated = true;
    [SerializeField] private GameObject rain;

    [SerializeField] private Animator calendarAnimator;
    private bool calendarActivated = true;
    [SerializeField] private GameObject calendar;

    [SerializeField] private Animator particlesAnimator;
    private bool particlesActivated = true;
    [SerializeField] private GameObject particles;

    private bool isCelcius = true;


    // WEATHER DISPLAY----------//
    public void setWeatherText(string weather, string temper, string city)
    {
        weatherTypeText.text = weather;
        temperatureText.text = temper;
        cityText.text = city;
    }

    // HOURLY WEATHER DISPLAY----------//
    public void SetHourlyWeather(Dictionary<string, List<string>> hourInfo)
    {
        fTemps = new string[temps.Length];
        cTemps = new string[temps.Length];

        int index = 0;
        foreach (KeyValuePair<string, List<string>> kvp in hourInfo)
        {
            if (index >= hours.Length)
                break;

            // Set hours
            hours[index].text = kvp.Key;
            
            // Grab the list
            List<string> retrievedData;
            retrievedData = kvp.Value;

            // Set temp to celcius to start
            temps[index].text = retrievedData[0];

            // Store temps to use later
            fTemps[index] = retrievedData[0];
            cTemps[index] = retrievedData[1];

            // Set weather
            weathers[index].text = retrievedData[2];

            index++;
        }
    }

    public void SetHourlyTemperature(bool celcius)
    {
        for (int i = 0; i < temps.Length; i++)
        {
            if (celcius)
                temps[i].text = cTemps[i];
            else
                temps[i].text = fTemps[i];            
        }
    }

    // OPTIONS-----------------//
    public void ToggleOptions()
    {
        optionsContent.SetActive(!optionsContent.activeSelf);
        
        if (optionsContent.activeSelf == true)
        {
            hourlyText.color = deSelectedColor;
            optionsText.color = selectedColor;
        }
        else
        {
            hourlyText.color = selectedColor;
            optionsText.color = deSelectedColor;
        }
    }

    public void ChangeUnits()
    {
        if(isCelcius)
        {
            temperatureText.text = retrieveWeather.fahrenheit.ToString("F1") + "°";
            SetHourlyTemperature(true);            
            isCelcius = false;
        }
        else
        {
            temperatureText.text = retrieveWeather.celcius.ToString("F1") + "°";
            SetHourlyTemperature(false);
            isCelcius = true;
        }
        

    }

    public void ShowCalendar()
    {
        if(calendarActivated)
        {
            calendar.SetActive(false);
            calendarAnimator.SetBool("Activate", false);
            calendarActivated = false;
        }
        else
        {
            calendar.SetActive(true);
            calendarAnimator.SetBool("Activate", true);
            calendarActivated = true;
        }                        
    }

    public void AllowAudio()
    {
        if (soundsActivated)
        {           
            AudioSource[] sources = rain.GetComponents<AudioSource>();
            foreach (AudioSource s in sources)
                s.enabled = false;

            soundsAnimator.SetBool("Activate", false);
            soundsActivated = false;
        }
        else
        {
            AudioSource[] sources = rain.GetComponents<AudioSource>();
            foreach (AudioSource s in sources)
                s.enabled = true;

            soundsAnimator.SetBool("Activate", true);
            soundsActivated = true;
        }
    }

    public void ShowParticles()
    {
        if (particlesActivated)
        {
            particles.SetActive(false);
            particlesAnimator.SetBool("Activate", false);
            particlesActivated = false;
        }
        else
        {
            particles.SetActive(true);
            particlesAnimator.SetBool("Activate", true);
            particlesActivated = true;
        }       
    }
}
