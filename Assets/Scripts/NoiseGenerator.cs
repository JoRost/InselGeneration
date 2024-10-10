using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    public static float[,] GenerateNoiseMap(int width, int height, float scale, Wave[] waves, Vector2 offset){
        float[,] noiseMap = new float[width, height];

        for(int x = 0; x < width; ++x){
            for(int y = 0; y < height; ++y){
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;

                float normalization = 0.0f;

                foreach(Wave wave in waves){
                    noiseMap[x, y] += wave.amplitude * Mathf.PerlinNoise(samplePosX * wave.frequency + wave.seed, samplePosY * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }

                if (normalization > 0.0f) noiseMap[x, y] /= normalization;
            }
        }

        return noiseMap;
    }

    public static float[,] GenerateCircleNoiseMap(int width, int height, float scale, Wave[] waves, Vector2 offset){
        float[,] noiseMap = new float[width, height];

        for(int x = 0; x < width; ++x){
            for(int y = 0; y < height; ++y){
                float distanceX = Math.Abs(x - width / 2);
                float distanceY = Math.Abs(y - height / 2);
                
                float samplePosX = (float)x * scale + offset.x;
                float samplePosY = (float)y * scale + offset.y;

                float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
                int maxWidth = width / 2 - 2;
                float delta = distance / maxWidth;
                float gradient = delta * delta;
            
            
                float normalization = 0.0f;

                foreach(Wave wave in waves){
                    noiseMap[x, y] += (float)Math.Max(0.0, 1.0 - gradient) * wave.amplitude * Mathf.PerlinNoise(samplePosX * wave.frequency + wave.seed, samplePosY * wave.frequency + wave.seed);
                    normalization += wave.amplitude;
                }
                if (normalization > 0.0f) noiseMap[x, y] /= normalization;
            }
        }

        return noiseMap;
    }
}

[System.Serializable]
public class Wave{
    public float seed;
    public float frequency;
    public float amplitude;
}