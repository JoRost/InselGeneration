using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine.Tilemaps;
using UnityEditor.UI;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Binome Preset")]
public class BinomePreset : ScriptableObject
{
    public RuleTile ruleTile;
    public float minHight;
    public int id;

    public RuleTile GetTile(){
        return ruleTile;
    }

    public string GetBinomeType(){
        return this.name;
    }

    public int GetID(){
        return id;
    }

    public bool matchesConditions(float height){
        return height >= minHight;
    }
}

public class BinomeTempData{
    public BinomePreset binome;

    public BinomeTempData(BinomePreset preset){
        binome = preset;
    }

    public float GetDiffValue(float height){
        return (height - binome.minHight);
    }
}
