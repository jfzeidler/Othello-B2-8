using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTextures : MonoBehaviour
{
    public Texture[] Textures;
    public int currentTextures;
    public GameObject Tile;

    public void TextureSwap()
    {
        currentTextures++;
        // currentTextures = currentTextures % textures.Length
        currentTextures %= Textures.Length;
        // Change the texture of the selected tile
        GetComponent<Renderer>().material.mainTexture = Textures[currentTextures];
    }
}
