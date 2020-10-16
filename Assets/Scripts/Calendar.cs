using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Globalization;

public class Calendar : MonoBehaviour
{

    [Header("Canvas Settings")]
    [SerializeField] private GameObject dayPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private float radius = 5;

    void Start()
    {
        DateTime myDT = DateTime.Now;
        GregorianCalendar gc = new GregorianCalendar();
                
        // Grabb all the info from today
        int dayOfMonth = gc.GetDayOfMonth(myDT);        
        string dayOfWeek = gc.GetDayOfWeek(myDT).ToString();
        string Month = gc.GetMonth(myDT).ToString();        
        int year = gc.GetYear(myDT);
        int daysInMonth = gc.GetDaysInMonth(year, gc.GetMonth(myDT));

        // Create arrays of daysOfWeek and daysOfMonth
        string[] daysOfWeek = new string[daysInMonth];
        int[] daysOfMonth = new int[daysInMonth];

        for (int i = 0; i < daysInMonth; i++)
        {
            // Find the days of the month
            myDT = DateTime.Now;
            myDT = myDT.AddDays(-dayOfMonth + i + 1);
            daysOfWeek[i] = gc.GetDayOfWeek(myDT).ToString();
            daysOfMonth[i] = i + 1;

            //Debug.Log(gc.GetDayOfWeek(myDT).ToString());
            //Debug.Log(i + 1);
        }

        SpawnCalendar(daysInMonth, daysOfWeek);

    }

    // Instantiate a day in its 3D world position
    public void SpawnCalendar(int numOfDays, string[] daysOfWeek)
    {
        for (int i = 0; i < numOfDays; i++)
        {
            float angle = i * Mathf.PI * 2f / numOfDays;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, transform.position.y, Mathf.Sin(angle) * radius);
            GameObject go = Instantiate(dayPrefab, newPos, Quaternion.identity);
            go.transform.SetParent(parent);

            go.GetComponent<Day>().AssignText((i + 1).ToString(), daysOfWeek[i], false);
        }
    }


}

