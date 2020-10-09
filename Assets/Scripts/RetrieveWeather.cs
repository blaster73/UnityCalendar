using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RetrieveWeather : MonoBehaviour
{

    [SerializeField] private RetrieveLocation retrieveLocation;

    private string baseUrl = "api.openweathermap.org/data/2.5/weather?lat=";
    private string preLon = "&lon=";
    private string preKey = "&appid=";
    private string apiKey = "547bc949f1ca09f43cfe8f9994a3bdf4";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Get Weather")]
    public void GetWeather()
    {
        StartCoroutine(GetWeatherRoutine(retrieveLocation.lat, retrieveLocation.lon));
    }

    IEnumerator GetWeatherRoutine(string lat, string lon)
    {

        int maxWait = 20;
        while (retrieveLocation.done == false && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        string url = baseUrl + lat + preLon + lon + preKey + apiKey;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);   

    }
}
