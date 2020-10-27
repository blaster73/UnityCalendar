using System.Collections;
using UnityEngine;
using TMPro;

public class RetrieveLocation : MonoBehaviour
{

    // Reference Unit Tester
    [SerializeField] private WeatherLocationUnitTest weatherLocationUnitTest;
    [SerializeField] private TMP_Text locationDebug;

    public string lat;
    public string lon;

    public bool done = false;

    public void StartFindingLocation()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            // Check to see if testing parameters should be used
            if (weatherLocationUnitTest.testLocation == true)
            {
                weatherLocationUnitTest.SetLocation();
            }
            done = true;
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Android");
            StartCoroutine(GetLocation());
        }

        StartCoroutine(GetLocation());
    }

    IEnumerator GetLocation()
    {
        // Start service before querying location
        Input.location.Start(500);

        // Wait until service initializes
        int maxWait = 5;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            //Debug.Log("Success");
        }

        if(Input.location.status == LocationServiceStatus.Stopped)
        {
            // Access granted and location value could be retrieved            
            lat = Input.location.lastData.latitude.ToString("F2");
            lon = Input.location.lastData.longitude.ToString("F2");
            Debug.Log(Input.location.lastData.longitude);
            locationDebug.text = lat + " : " + lon;
        }

        Debug.Log("End of GetLocation routine");

        // Stop service if there is no need to query location updates continuously
        done = true;
        Input.location.Stop();
    }
}
