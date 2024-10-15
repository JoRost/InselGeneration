using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
// using System.Text;

public class Map : MonoBehaviour
{
    public TilemapPreset[] tileMapPresets;

    [Header("Dimensions")]
    public int width = 50;
    public int height = 50;
    public float scale = 1.0f;
    public Vector2 offset = Vector2.zero;

    [Header("Height Map")]
    public Wave[] heightWaves;
    public float[,] heightMap;

    private int[,] tileMapData;

    void GenerateMap(){
        heightMap = NoiseGenerator.GenerateCircleNoiseMap(width, height, scale, heightWaves, offset);
        tileMapData = new int[width, height];

        foreach(TilemapPreset tilemapPreset in tileMapPresets){
            tilemapPreset.createTilemap();
            for(int x = 0; x < width; ++x){
                for(int y = 0; y < height; ++y){
                    int lowerBiome = tileMapData[x, y] != 0 ? tileMapData[x, y] : -1;
                    BinomePreset currentBiome = GetBinome(heightMap[x, y], tilemapPreset.GetBinomes(), tilemapPreset.matchesConditions(lowerBiome));

                    if(currentBiome != null){
                        tilemapPreset.GetTilemap().SetTile(new Vector3Int(x, y, 0), currentBiome.GetTile());
                        tileMapData[x, y] = currentBiome.GetID();
                    }
                }
            }
        }
        
        // StringBuilder sb = new StringBuilder();
        // for(int i=0; i< tileMapData.GetLength(1); i++)
        // {
        //     for(int j=0; j<tileMapData.GetLength(0); j++)
        //     {
        //         sb.Append(tileMapData[i,j]);
        //         sb.Append(' ');				   
        //     }
        //     sb.AppendLine();
        // }
        // Debug.Log(sb.ToString());
    }

    private void Start() {
        GenerateMap();
    }


    [ContextMenu("Regenerate Map")]
    void RegenerateMap(){
        foreach(TilemapPreset tilemapPreset in tileMapPresets){tilemapPreset.GetTilemap().ClearAllTiles();}
        GenerateMap();
    } 

    private BinomePreset GetBinome(float height, BinomePreset[] biomes, bool canBePlacedOnLower){
        List <BinomeTempData> binomeTemp = new List<BinomeTempData>();

        foreach(BinomePreset binome in biomes){
            if(binome.matchesConditions(height) && canBePlacedOnLower){
                binomeTemp.Add(new BinomeTempData(binome));
            }
        }

        float curVal = 0.0f;
        BinomePreset binomeToReturn = null; 
        foreach(BinomeTempData binome in binomeTemp){
            if(binomeToReturn == null){
                binomeToReturn = binome.binome;
                curVal = binome.GetDiffValue(height);
            } else {
                if(binome.GetDiffValue(height) < curVal){
                    binomeToReturn = binome.binome;
                    curVal = binome.GetDiffValue(height);
                }
            }
        }

        return binomeToReturn;
    }
}
