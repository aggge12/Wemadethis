using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector3Int CurrentTile = Vector3Int.zero;
    public Vector3Int NextTile =  Vector3Int.zero;
    public Vector3 QueuedPosition = new Vector3(0,0,0);

    public List<Vector3Int> Path = new List<Vector3Int>();

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

    public void MoveToNextCell(Grid grid, float duration){
        var to = Path.First();
        CurrentTile = to;

        var newPosition = grid.GetCellCenterWorld(to);
        LinearInterpolation.Set(transform.position, newPosition, duration);

        Path.RemoveAt(0);
        movementIsQueued = false;
    }

    public void SetPath(List<Vector3Int> path){
        this.Path = path;
        this.NextTile = path.First();
    }

    public void SetNewDestination(Vector2 destination){
        Debug.Log("new destination");
        Debug.Log(destination);
        QueuedPosition = destination;
        movementIsQueued = true;
        Path = new List<Vector3Int>();
    }
}
