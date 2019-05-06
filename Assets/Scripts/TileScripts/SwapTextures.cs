using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTextures : MonoBehaviour
{
    public Texture[] textures;
    public int currentTextures;
    public GameObject tile;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TextureSwap()
    {
        currentTextures++;
        // currentTextures = currentTextures % textures.Length
        currentTextures %= textures.Length;
        // Change the texture of the selected tile
        GetComponent<Renderer>().material.mainTexture = textures[currentTextures];
    }
}
