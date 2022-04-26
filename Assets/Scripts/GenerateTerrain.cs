using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public int dirtLayerHeight = 5;

    public Sprite grass;
    public Sprite dirt;
    public Sprite stone;
    public Sprite coin;

    public int coinChance;
    public bool generateCaves = true;
    public float surfaceValue = 0.25f;
    public int worldSize = 100;
    public float caveFreq = 0.05f;
    public float terrainFreq = 0.05f;
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
            float height = Mathf.PerlinNoise((x + seed) * terrainFreq, seed * terrainFreq) * heightMultiplier + heightAddition;

            for(int y = 0; y < height; y++)
            {
                Sprite tileSprite;
                if (y < height - dirtLayerHeight)
                {
                    tileSprite = stone;
                }
                else if(y < height - 1)
                {
                    tileSprite = dirt;
                }
                else
                {
                    tileSprite = grass;

                    int c = Random.Range(0, coinChance);

                    if (c == 1)
                    {
                        GenerateCoin(x, y + 1);
                    }
                }

                if (generateCaves)
                {


                    if (noiseTexture.GetPixel(x, y).r > surfaceValue)
                    {
                        PlaceTile(tileSprite, x, y, false);

                    }
                }
                else
                {
                    PlaceTile(tileSprite, x, y, false);
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
                float v = Mathf.PerlinNoise(x * caveFreq, y * caveFreq);
                noiseTexture.SetPixel(x, y, new Color(v, v, v));
            }
        }

        noiseTexture.Apply();
    }

    void GenerateCoin(int x, int y)
    {
        PlaceTile(coin, x, y, true);
    }

    public void PlaceTile(Sprite tileSprite, int x, int y, bool backgroundElement)
    {
        GameObject newTile = new GameObject();
        newTile.transform.parent = this.transform;
        if (!backgroundElement)
        {
            newTile.AddComponent<BoxCollider2D>();
            newTile.GetComponent<BoxCollider2D>().size = Vector2.one;
            newTile.tag = "Ground";
        }
        
        newTile.AddComponent<SpriteRenderer>();
        newTile.GetComponent<SpriteRenderer>().sprite = tileSprite;
        newTile.name = tileSprite.name;
        newTile.transform.position = new Vector2(x + 0.5f, y + 0.5f);
    }
}
