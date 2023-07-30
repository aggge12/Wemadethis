using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 CurrentTile = new Vector2(0,0);
    public Vector2 NextTile = new Vector2(0,0);
    public Vector2 QueuedMovementTile = new Vector2(0,0);
    public Vector3 QueuedPosition = new Vector3(0,0,0);
    public LinearInterpolation LinearInterpolation;

    public bool movementIsQueued = false;

    // Start is called before the first frame update
    void Start()
    {
        LinearInterpolation = gameObject.AddComponent<LinearInterpolation>();
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    public void SetMovement(Vector3 to, float duration){
        LinearInterpolation.Set(transform.position, to, duration);
        CurrentTile = NextTile;

        SetNewTileInPathfinding();

        movementIsQueued = false;
    }

    public void SetNewTileInPathfinding(){

    }

    public void SetNewDestination(Vector2 destination){
        Debug.Log("new destination");
        Debug.Log(destination);
        QueuedPosition = destination;
        movementIsQueued = true;
    }
}
