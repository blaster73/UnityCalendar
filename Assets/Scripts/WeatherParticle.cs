using UnityEngine;
using DigitalRuby.RainMaker;

public class WeatherParticle : MonoBehaviour
{

    // Bring in the asset "Rain Maker (c) 2015 Digital Ruby, LLC"
    [SerializeField] private RainScript rainScript;

    [SerializeField] private GameObject cloudsLight;
    [SerializeField] private GameObject cloudsDark;

    [SerializeField] private Light directionLight;
    [SerializeField] private Color clearLight;
    [SerializeField] private Color stormLight;

    public void SetWeather(string mainCondition, string mainDescription)
    {

        //Debug.Log("Called SetWeather");
        //Debug.Log(mainCondition);

        if (mainCondition.Contains("Clear"))
        {
            Debug.Log("Clear");
        }

        if(mainCondition.Contains("Thunderstorm"))
        {
            Debug.Log("Thunderstorm");
            rainScript.RainIntensity = 1;
            cloudsDark.SetActive(true);
            directionLight.color = stormLight;
            RenderSettings.fogStartDistance = 0;
        }

        if (mainCondition.Contains("Rain"))
        {
            Debug.Log("Rain");
            rainScript.RainIntensity = 0.5f;
            cloudsLight.SetActive(true);        
            RenderSettings.fogStartDistance = 6;
        }

        if (mainCondition.Contains("Drizzle"))
        {
            Debug.Log("Drizzle");
            rainScript.RainIntensity = 0.25f;
        }

        if (mainCondition.Contains("Clouds"))
        {
            Debug.Log("Clouds");
            cloudsLight.SetActive(true);
            RenderSettings.fogStartDistance = 6;
        }

        // Set fog close to the camera
        if (mainCondition.Contains("Mist") | mainCondition.Contains("Smoke") | mainCondition.Contains("Haze") | mainCondition.Contains("Dust") | mainCondition.Contains("Fog"))
        {
            Debug.Log("Mist, etc");
            RenderSettings.fogStartDistance = 0;
        }
    }
}
