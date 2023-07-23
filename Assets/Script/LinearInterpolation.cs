using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInterpolation : MonoBehaviour
{
    private Vector2 start;
    private Vector2 end;
    private float startTime;
    private float endTime;
    private bool finished;

    public delegate void movementFinishedDelegate();
    public movementFinishedDelegate onFinish;

    public Vector2 velocity;
    public bool disabled;

    void Start(){

    }

    void Update()
    {
        if (!disabled){
            if (Time.time < startTime + endTime){
                //snabb början, långsam slut (sqrt(x))
/*                 var time = Mathf.Sqrt(1/endTime * (Time.time - startTime));
                transform.position = ((1-time) * start) + (time * end); */

                //snabbare i början lånsammare i slutet (sqrt(1-1(x-1)^2)), Min favorit so far
                var time = Mathf.Sqrt(1-Mathf.Pow((1/endTime * (Time.time - startTime))-1, 2));
                transform.position = ((1-time) * start) + (time * end);

                
            } else if (!finished) {
                finished = true;
                if (onFinish != null){
                    onFinish();
                }
            }
        }
    }

    public void Stop(bool x, bool y){

    }

    public void Set(Vector2 start, Vector2 end, float endTime, movementFinishedDelegate onFinish = null){
        this.start = start;
        this.end = end;
        this.startTime = Time.time;
        this.endTime = endTime;
        this.onFinish = onFinish;
        finished = false;
    }
}
