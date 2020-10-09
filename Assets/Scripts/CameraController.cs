using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject player;

    public float rotationSpeed = 0.1f;

    private bool mouseDown;
    private float previousPosition;
    private float currentPosition; 

    void Update()
    {
        currentPosition = Input.mousePosition.x;
        if (Input.GetMouseButton(0))
        {
            

            float angle = (previousPosition - currentPosition) * Mathf.PI * rotationSpeed;
            transform.RotateAround(player.transform.position, -Vector3.up, angle);

            
        }
        previousPosition = Input.mousePosition.x;

        /*if (Input.GetMouseButtonUp(0))
        {
            previousPosition = Input.mousePosition.x;
        }*/
    }

}
