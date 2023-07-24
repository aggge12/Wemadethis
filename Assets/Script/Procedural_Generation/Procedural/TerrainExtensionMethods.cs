using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainExtensionMethods
{
    public static void ChangePixel(this Texture2D texture2D, int x, int y, float r, float g, float b){
        var pixel = texture2D.GetPixel(x,y);

        pixel.r += r;
        pixel.g += g;
        pixel.b += b;

        texture2D.SetPixel(x,y, pixel);
    }
}
