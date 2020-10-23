using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Slider : EventTrigger
{

    private Vector2 startingPos;
    private float upperYLimit = 2357;    
    private float lerpSpeed = 10;
    private float yDistance;
    private float currentThreshold;

    private float upperThreshold;
    private float lowerThreshold;

    private bool dragging;

    public void Start()
    {
        // Calculate start y, total distance y can move, and current threshold for lerping
        startingPos = transform.position;
        yDistance = (upperYLimit - startingPos.y);
        currentThreshold = yDistance * 0.25f;

        // calculate drag release thresholds
        upperThreshold = upperYLimit - 100;
        lowerThreshold = startingPos.y + 100;
    }

    public void Update()
    {        
        if (dragging)
        {
            // Lock window on the X axis
            transform.position = new Vector2(startingPos.x, Input.mousePosition.y);
            
            // Lock window from moving too low or too high
            if(transform.position.y < startingPos.y)            
                transform.position = startingPos;            

            if(transform.position.y > upperYLimit)            
                transform.position = new Vector2(startingPos.x, upperYLimit);                        
        }
        else
        {
            // Lerp window to top or bottom if it gets close enough
            if(transform.position.y < currentThreshold)
            {
                float yPos = Mathf.Lerp(transform.position.y, startingPos.y, lerpSpeed * Time.deltaTime);
                transform.position = new Vector2(startingPos.x, yPos);
                if (transform.position.y <= lowerThreshold)
                {
                    currentThreshold = yDistance * 0.25f;
                }
                
            }
            else
            {
                float yPos = Mathf.Lerp(transform.position.y, upperYLimit, lerpSpeed * Time.deltaTime);
                transform.position = new Vector2(startingPos.x, yPos);
                if (transform.position.y >= upperThreshold)
                {
                    currentThreshold = yDistance * 0.85f;
                }
            }
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}