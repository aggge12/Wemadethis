using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;


public class Place_Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public TileBase earth_Tile;

    public float seed;

    [Header("Generation Settings")]
    public int worldSize = 100;


    [Header("Noise Textures")]
    public Texture2D foundation;
    public float foundationRarity = 0.01f;
    public float foundationSize = 0.02f;
    public Texture2D biome;
    public float biomeRarity = 0.03f;
    public float biomeSize = 0.04f;
    public Texture2D element;
    public float elementRarity = 0.05f;
    public float elementSize = 0.06f;


    void Start()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition);
        Tilemap tilemap = GetComponent<Tilemap>(); 
        tilemap.SetTile((new Vector3Int(0,0,0)), earth_Tile);

        seed = Random.Range(-10000, 10000);

        foundation = new Texture2D(worldSize, worldSize);
        biome = new Texture2D(worldSize, worldSize);
        element = new Texture2D(worldSize, worldSize);

        GenerateNoiseTexture(foundationRarity, foundationSize, foundation);
        GenerateNoiseTexture(biomeRarity, biomeSize, biome);
        GenerateNoiseTexture(elementRarity, elementSize, element);
        Debug.Log("Generated on Start");
    }

    // Update is called once per frame

    public void GenerateNoiseTexture(float frequency, float limit, Texture2D noiseTexture) 
    {
        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency);
                if (v > limit*1.1)
                    noiseTexture.SetPixel(x, y, Color.green);
                else
                    noiseTexture.SetPixel(x, y, Color.black);
            }
        }

        noiseTexture.Apply();
    }
    public void PlaceTile(TileBase tileSprite, int x, int y)
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTile((new Vector3Int(x,y,0)), tileSprite);
    }
}
