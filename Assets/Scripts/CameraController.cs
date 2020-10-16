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

    public bool ended = false;

    void Update()
    {
        // PC
        /*currentPosition = Input.mousePosition.x;
        if (Input.GetMouseButton(0))
        {            
            float angle = (previousPosition - currentPosition) * Mathf.PI * rotationSpeed;
            transform.RotateAround(player.transform.position, -Vector3.up, angle);            
        }
        previousPosition = Input.mousePosition.x;
        */

        // Mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                if (ended)
                {
                    previousPosition = touch.position.x;
                    ended = false;
                }

                currentPosition = touch.position.x;
                float angle = (previousPosition - currentPosition) * Mathf.PI * rotationSpeed;
                transform.RotateAround(player.transform.position, -Vector3.up, angle);
                previousPosition = touch.position.x;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                ended = true;
            }
        }

    }

}
