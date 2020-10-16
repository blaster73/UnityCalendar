using UnityEngine;
using TMPro;

public class MainCanvas : MonoBehaviour
{    

    [Header("Canvas")]
    [SerializeField] private TMP_Text weatherTypeText;
    [SerializeField] private TMP_Text temperatureText;
    [SerializeField] private TMP_Text cityText;

    [Header("Options")]
    [SerializeField] private RetrieveWeather retrieveWeather;
    [SerializeField] private GameObject optionsMenu;
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


    // OPTIONS BUTTONS----------//
    public void EnableOptions()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void ChangeUnits()
    {
        if(isCelcius)
        {
            temperatureText.text = retrieveWeather.fahrenheit.ToString("F1") + "°";
            isCelcius = false;
        }
        else
        {
            temperatureText.text = retrieveWeather.celcius.ToString("F1") + "°";
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
