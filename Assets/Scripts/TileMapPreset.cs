using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Presets;
using UnityEngine.Tilemaps;
using UnityEditor.UI;
using System.Linq;
using System;


[CreateAssetMenu(fileName = "TileMap Preset", menuName = "New TileMap Preset")]
public class TilemapPreset : ScriptableObject
{
    public BinomePreset[] biomes;
    public int[] allowedLowerBiomes;

    [Header("Height Map")]
    public Wave[] heightWaves;

    [HideInInspector]
    public Tilemap tilemap;

    public void createTilemap(){
        tilemap = new GameObject(this.name).AddComponent<Tilemap>();
        tilemap.gameObject.AddComponent<TilemapRenderer>();
        tilemap.gameObject.transform.parent = GameObject.Find("Grid").transform;
    }

    public bool matchesConditions(int lowerBiome){
        return ((lowerBiome == -1) || (allowedLowerBiomes.Contains(lowerBiome)));
    }

    public Tilemap GetTilemap(){
        return tilemap;
    }

    public BinomePreset[] GetBinomes(){
        return biomes;
    }
}
