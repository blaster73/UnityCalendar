using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using TMPro;

public class GetLocationPermission : MonoBehaviour
{

    // Reference RetrieveWeather
    [SerializeField] private RetrieveWeather retrieveWeather;    

    public bool locationEnabled = false;
    [SerializeField] private TMP_Text debugText;
    [SerializeField] private float pretendWaitTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.CoarseLocation))
        {
            Permission.RequestUserPermission(Permission.CoarseLocation);
            //dialog = new GameObject();
        }
        #endif

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            // Check to see if testing parameters should be used
            StartCoroutine(PretendAccessDelay());
        }
    }

    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Permission.HasUserAuthorizedPermission(Permission.CoarseLocation) || Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Debug.Log("starting Getting Weather + Location");
                debugText.text = "starting Getting Weather + Location";
                StartGettingLocWeather();
            }
            else
            {
                Debug.Log("Can't check permissions");
                debugText.text = "Can't check permissions";
            }
        }
        else
        {
            debugText.text = "Not Android";
        }
    }


    // Simulate a user taking some time to enable location 
    IEnumerator PretendAccessDelay()
    {
        yield return new WaitForSeconds(pretendWaitTime);
        StartGettingLocWeather();
    }

    void StartGettingLocWeather()
    {
        locationEnabled = true;
        retrieveWeather.GetWeather();        
        Destroy(this);
    }
}
