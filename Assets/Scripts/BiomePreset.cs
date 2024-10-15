using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Binome Preset")]
public class BinomePreset : ScriptableObject
{
    public Sprite[] tiles;
    public RuleTile ruleTile;
    public float minHight;
    public float minMoisture;

    public RuleTile GetTile(){
        // return tiles[Random.Range(0, tiles.Length)];
        return ruleTile;
    }

    public bool matchesConditions(float height, float moisture){
        return height >= minHight && moisture >= minMoisture;
    }
}

public class BinomeTempData{
    public BinomePreset binome;

    public BinomeTempData(BinomePreset preset){
        binome = preset;
    }

    public float GetDiffValue(float height, float moisture){
        return ((height - binome.minHight) + (moisture - binome.minMoisture));
    }
}
