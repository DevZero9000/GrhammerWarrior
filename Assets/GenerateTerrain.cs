using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public Sprite tile;
    public int worldSize = 100;
    public float noiseFreq = 0.05f;
    public float heightMultiplier = 4f;
    public int heightAddition = 25;
    public float seed;
    public Texture2D noiseTexture;

    private void Start()
    {
        seed = Random.Range(-10000, 10000);
        GenerateNoiseTexture();
        TerrainGenerate();
    }

    public void TerrainGenerate()
    {
        for(int x = 0; x < worldSize; x++)
        {
            float height = Mathf.PerlinNoise((x + seed) * noiseFreq, seed * noiseFreq) * heightMultiplier + heightAddition;

            for(int y = 0; y < height; y++)
            {

                if (noiseTexture.GetPixel(x, y).r < 0.5f)
                {
                    GameObject newTile = new GameObject(name = "tile");
                    newTile.transform.parent = this.transform;
                    newTile.AddComponent<SpriteRenderer>();
                    newTile.GetComponent<SpriteRenderer>().sprite = tile;
                    newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);

                }
            }
        }
    }

    public void GenerateNoiseTexture()
    {
        noiseTexture = new Texture2D(worldSize, worldSize);

        for (int x = 0; x < noiseTexture.width; x++)
        {
            for (int y = 0; y < noiseTexture.height; y++)
            {
                float v = Mathf.PerlinNoise(x * noiseFreq, y * noiseFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }
}
