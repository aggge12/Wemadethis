using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameEvents : MonoBehaviour
{
    private float interval = 0.333f;
    private GameObject[] players;
    private Pathfinding pathfinding;
    private Place_Tile placeTile;

    public bool Initialized;

    private void Start()
    {
        pathfinding = gameObject.AddComponent<Pathfinding>();
        placeTile = GetComponentInChildren<Place_Tile>();
        players = GameObject.FindGameObjectsWithTag("Player");
        InvokeRepeating("UpdateGameState", 1f, interval);
    }

    void Update()
    {
        if (placeTile.Initialized && !Initialized){
            Initialized = true;
            var tilemap = GetComponentInChildren<Tilemap>();
            pathfinding.InitializeGrid(tilemap);
        }
    }

    public Vector3 TranslateMousePosition(Vector2 newPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane zPlane = new Plane(Vector3.forward, Vector3.zero);

        if (zPlane.Raycast(ray, out float distance))
        {
            var point =  ray.GetPoint(distance);
            Debug.Log("Hit");
            Debug.Log(point);

            return point;    
        }

        Debug.Log("noHit");

        return Vector3.zero;
    }

    private void UpdateGameState() {
        foreach (GameObject player in players)
        {
            var component = player.GetComponent<Move>();

            if (component != null)
            {
                var grid = transform.GetComponent<Grid>();
                if (component.movementIsQueued && component.Path.Count == 0){
                    var worldPosition = Camera.main.ScreenToWorldPoint(component.QueuedPosition);
                    var translatedPosition = TranslateMousePosition(worldPosition);
                    var cellPosition = grid.WorldToCell(translatedPosition);
                    var path = pathfinding.FindPath(component.CurrentTile, cellPosition);

                    if (path.Count > 0){
                        component.SetPath(path);
                    }
                }
                if (component.Path.Count > 0){
                    component.MoveToNextCell(grid, 1);
                }
            }
            else
            {
                Debug.LogWarning("Desired component not found on player: " + player.name);
            }
        }
    }
}
