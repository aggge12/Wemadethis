using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    private Vector2 start;
    private Vector2 end;
    private float startTime;
    private bool finished = true;
    private float journeyLength;
    private float duration;

    public delegate void movementFinishedDelegate();
    public movementFinishedDelegate onFinish;

    public Vector2 velocity;
    public bool disabled;

    private void Start()
    {
    }

    private void Update()
    {
        if (!finished){
            float fractionOfJourney = (Time.time - startTime) / duration;
            transform.position = Vector2.Lerp(start, end, fractionOfJourney);
        
            if (Time.time >= duration + startTime){
                finished = true;
            }

            Debug.Log("test");
        }
 
    }

    public void Set(Vector2 start, Vector2 end, float duration, movementFinishedDelegate onFinish = null){
        this.start = start;
        this.end = end;
        this.startTime = Time.time;
        this.onFinish = onFinish;
        this.duration = duration;
        this.finished = false;
    }
}
