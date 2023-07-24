using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileAtlas", menuName = "Tile Atlas")]
public class TileAtlas : ScriptableObject
{
    public TileClass electric;
    public TileClass water;
    public TileClass fire;
    public TileClass wind;
    public TileClass earth;

}

