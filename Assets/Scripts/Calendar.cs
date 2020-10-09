using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Globalization;

public class Calendar : MonoBehaviour
{

    [SerializeField] private CalendarSpawner calendarSpawner;

    void Start()
    {
        DateTime myDT = DateTime.Now;

        GregorianCalendar gc = new GregorianCalendar();

        // Displays the values of the DateTime.
        Debug.Log(gc.GetDayOfMonth(myDT));
        Debug.Log(gc.GetDayOfWeek(myDT));
        Debug.Log(gc.GetMonth(myDT));
        Debug.Log(gc.GetYear(myDT));
        Debug.Log(" ");
        Debug.Log(gc.GetDaysInMonth(gc.GetYear(myDT), gc.GetMonth(myDT)));

        int numOfDays = gc.GetDaysInMonth(gc.GetYear(myDT), gc.GetMonth(myDT));
        calendarSpawner.SpawnCalendar(numOfDays);

        //DisplayValues(myCal, myDT);
    }

    void Update()
    {

    }

}

