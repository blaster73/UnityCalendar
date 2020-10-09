using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrieveLocation : MonoBehaviour
{

    public string lat;
    public string lon;

    public bool done = false;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("Windows");
            lat = "28.53";
            lon = "-81.37";
            done = true;
        }
        else if(Application.platform == RuntimePlatform.Android)
        {
            Debug.Log("Android");
            StartCoroutine(GetLocation());
        }
        else
        {
            Debug.Log("???????????");
        }
    }

    IEnumerator GetLocation()
    {
        Debug.Log("Actually started");

        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

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
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stop service if there is no need to query location updates continuously
        done = true;
        Input.location.Stop();
    }
}
