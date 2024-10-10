using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    public BinomePreset[] biomes;
    public GameObject tilePrefab;
    
    private List<GameObject> tileList = new List<GameObject>(); 

    [Header("Dimensions")]
    public int width = 50;
    public int height = 50;
    public float scale = 1.0f;
    public Vector2 offset = Vector2.zero;

    [Header("Height Map")]
    public Wave[] heightWaves;
    public float[,] heightMap;

    [Header("Moisture Map")]
    public Wave[] moistureWaves;
    public float[,] moistureMap;

    void GenerateMap(){
        heightMap = NoiseGenerator.GenerateCircleNoiseMap(width, height, scale, heightWaves, offset);
        moistureMap = NoiseGenerator.GenerateNoiseMap(width, height, scale, moistureWaves, offset);

        for(int x = 0; x < width; ++x){
            for(int y = 0; y < height; ++y){
                GameObject tile = Instantiate(tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.GetComponent<SpriteRenderer>().sprite = GetBinome(heightMap[x, y], moistureMap[x, y]).GetTileSprite();
                tileList.Add(tile);
            }
        }
    }

    private void Start() {
        GenerateMap();
    }


    [ContextMenu("Regenerate Map")]
    void RegenerateMap(){
        foreach(GameObject tile in tileList){
           Destroy(tile);
        }
        GenerateMap();
    } 

    private BinomePreset GetBinome(float height, float moisture){
        List <BinomeTempData> binomeTemp = new List<BinomeTempData>();

        foreach(BinomePreset binome in biomes){
            if(binome.matchesConditions(height, moisture)){
                binomeTemp.Add(new BinomeTempData(binome));
            }
        }

        float curVal = 0.0f;
        BinomePreset binomeToReturn = null; 
        foreach(BinomeTempData binome in binomeTemp){
            if(binomeToReturn == null){
                binomeToReturn = binome.binome;
                curVal = binome.GetDiffValue(height, moisture);
            } else {
                if(binome.GetDiffValue(height, moisture) < curVal){
                    binomeToReturn = binome.binome;
                    curVal = binome.GetDiffValue(height, moisture);
                }
            }
        }

        if(binomeToReturn == null) binomeToReturn = biomes[0];

        return binomeToReturn;
    }
}
