using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarSpawner : MonoBehaviour
{
    [SerializeField] private int numObjects = 10;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    [SerializeField] private float radius = 5;

    public void SpawnCalendar(int numOfDays)
    {
        for (int i = 0; i < numOfDays; i++)
        {
            float angle = i * Mathf.PI * 2f / numOfDays;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, transform.position.y, Mathf.Sin(angle) * radius);
            GameObject go = Instantiate(prefab, newPos, Quaternion.identity);
            go.transform.SetParent(parent);
        }
    }

}
