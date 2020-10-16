using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Day : MonoBehaviour
{

    [SerializeField] private Transform mainCamera;

    [SerializeField] private TMP_Text dayNumber;
    [SerializeField] private TMP_Text dayName;


    void Start()
    {
        // Find the camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    
    void Update()
    {
        // Always look at the camera
        transform.LookAt(2 * transform.position - mainCamera.position);
    }

    public void AssignText(string dayOfMonth, string dayOfWeek, bool isToday)
    {
        // Assign text to the canvas elements
        dayNumber.text = dayOfMonth;
        dayName.text = dayOfWeek;
    }
}
