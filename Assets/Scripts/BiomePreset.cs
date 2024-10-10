using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Binome Preset")]
public class BinomePreset : ScriptableObject
{
    public Sprite[] tiles;
    public float minHight;
    public float minMoisture;

    public Sprite GetTileSprite(){
        return tiles[Random.Range(0, tiles.Length)];
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
