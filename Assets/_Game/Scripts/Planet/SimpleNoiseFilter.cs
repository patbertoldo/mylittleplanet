using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter
{
    private SimpleNoiseSettings noiseSettings;
    private FastNoise noise = new FastNoise();

    public SimpleNoiseFilter(SimpleNoiseSettings noiseSettings)
    {
        this.noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = noiseSettings.BaseRoughness;
        float amplitude = 1;

        for (int i = 0; i < noiseSettings.NumberOfLayers; i++)
        {
            float v = noise.GetSimplex(
                point.x * frequency + noiseSettings.Centre.x,
                point.y * frequency + noiseSettings.Centre.y,
                point.z * frequency + noiseSettings.Centre.z);

            // Multiply by 0.5f to get into the range 0 to 1.
            noiseValue += (v + 1) * 0.5f * amplitude;

            // When Roughness is > 1 the frequency will increase with each layer.
            frequency *= noiseSettings.Roughness;

            // When Persistence is < 1 amplitude will decrease with each layer.
            amplitude *= noiseSettings.Persistence;
        }

        noiseValue = noiseValue - noiseSettings.Minimum;

        return noiseValue * noiseSettings.Strength;
    }
}
