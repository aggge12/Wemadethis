using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;


public class Place_Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public TileBase earth_Tile;

    void Start()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition);
        Tilemap tilemap = GetComponent<Tilemap>(); 
        tilemap.SetTile((new Vector3Int(0,0,0)), earth_Tile);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaceTile(TileBase tileSprite, int x, int y)
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTile((new Vector3Int(x,y,0)), tileSprite);
    }
}
