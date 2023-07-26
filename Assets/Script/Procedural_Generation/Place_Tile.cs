using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using System.Linq;


public class Place_Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public TileBase earth_Tile;

    public float seed;

    [Header("Generation Settings")]
    public int worldSize = 100;


    [Header("Noise Textures")]
    public Texture2D miniMap;
    public Vector2 spawn;
    public Vector2 origin;
    public float edge = 0.3f;
    public Texture2D foundation;
    public float foundationRarity = 0.015f;
    public float foundationSize = 0.2f;
    public Texture2D biome;
    public float biomeRarity = 0.03f;
    public float biomeSize = 0.04f;
    public Texture2D testMap;
    public float testRarity = 0.05f;
    public float testSize = 0.06f;


    void Start()
    {
        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
        transform.position = gridLayout.CellToWorld(cellPosition);
        Tilemap tilemap = GetComponent<Tilemap>(); 
        tilemap.SetTile((new Vector3Int(0,0,0)), earth_Tile);

        seed = Random.Range(-10000, 10000);

        miniMap = new Texture2D(worldSize, worldSize);
        foundation = new Texture2D(worldSize, worldSize);
        biome = new Texture2D(worldSize, worldSize);
        testMap = new Texture2D(worldSize, worldSize);

        GenerateNoiseTexture(foundationRarity, foundationSize, foundation);
        GenerateNoiseTexture(biomeRarity, biomeSize, biome);
        GenerateNoiseTexture(testRarity, testSize, testMap);

        MiniMapMaker();

    }

    // Update is called once per frame
    private void Update() {
        GenerateNoiseTexture(testRarity, testSize, testMap);
    }

    public void GenerateNoiseTexture(float frequency, float limit, Texture2D noiseTexture) 
    {
        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise((x + seed) * frequency, (y + seed) * frequency);
                if (v > limit){
                    noiseTexture.SetPixel(x, y, new Color(0,v,0));
                }
                else{
                    noiseTexture.SetPixel(x,y, Color.black);
                }


            }
        }

        noiseTexture.Apply();
    }
    public void MiniMapMaker()
    {
        float p1;
        float p2;
        float px;
        float gpx = 0.5f;
        Vector2 poi = new Vector2(worldSize,worldSize);
        spawn = new Vector2(worldSize/2, worldSize/2);
        origin = new Vector2(worldSize/2, worldSize/2);

        for (int y = 0; y <= worldSize/2; y++)
        {
            for (int x = 0; x <= worldSize; x++)
            {
                p1 = foundation.GetPixel(x, y).g;
                p2 = foundation.GetPixel(worldSize-x, worldSize-y).g;
                if (Mathf.Sqrt(Mathf.Pow(x-worldSize/2, 2) + Mathf.Pow(y-worldSize/2, 2)) > worldSize/2 * (1 - edge)){
                    px = (p1+p2)/2 * (1-(Mathf.Sqrt(Mathf.Pow(x-worldSize/2, 2)+Mathf.Pow(y-worldSize/2, 2)) - worldSize/2*(1-edge))/(worldSize/2 * edge));
                } else {
                    px = (p1+p2)/2;
                }
                if (px > 0.5f){
                    miniMap.SetPixel(x, y, new Color(0f, px, 0f));
                    miniMap.SetPixel(worldSize-x, worldSize-y, new Color(0f, px, 0f));
                } else {
                    miniMap.SetPixel(x, y, new Color(0f, 0f, 0f));
                    miniMap.SetPixel(worldSize-x, worldSize-y, new Color(0f, 0f, 0f));
                }
                if (px >= gpx && Vector2.Distance(new Vector2(x,y), origin) > Vector2.Distance(spawn, origin)){
                    gpx = px;
                    spawn = new Vector2(x,y);
                }
            }
        }
        for (int y = (int)spawn.y - 5; y <= (int)spawn.y + 5; y++){
            for (int x = (int)spawn.x - 5; x <= (int)spawn.x + 5; x++){
                if (Vector2.Distance(new Vector2(x,y), spawn) <= 5){
                    miniMap.SetPixel(x, y, new Color(1f, 1f, 0));
                    miniMap.SetPixel(worldSize-x, worldSize-y, new Color(1f, 1f, 0));
                }
            }
        }

        Vector2 dir = (spawn - origin).normalized;
        Vector2 start = origin;
        Quaternion rotation = Quaternion.Euler(0, 0, 30);
        Vector3 tempDir;
        for (int i = 0; i < 5; i++){
            tempDir = rotation * new Vector3(dir.x, dir.y, 0);
            while (Vector2.Distance(start, origin) < worldSize/2){
                dir = new Vector2(tempDir.x, tempDir.y).normalized;
                start += dir;
                if (miniMap.GetPixel(Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y)) != Color.black){
                    miniMap.SetPixel(Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y), new Color(1f, 1f, 0));
                }

            }
            start = origin;
        }


        miniMap.Apply();
    }

    public void RoadMaker(Vector2 p1, Vector2 p2){
        
    }


    public void PlaceTile(TileBase tileSprite, int x, int y)
    {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.SetTile((new Vector3Int(x,y,0)), tileSprite);
    }
}
