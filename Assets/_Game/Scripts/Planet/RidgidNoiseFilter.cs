using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgidNoiseFilter : INoiseFilter
{
    private RidgidNoiseSettings noiseSettings;
    private FastNoise noise = new FastNoise();

    public RidgidNoiseFilter(RidgidNoiseSettings noiseSettings)
    {
        this.noiseSettings = noiseSettings;
    }

    public float Evaluate(Vector3 point, int seed)
    {
        float noiseValue = 0f;
        float frequency = noiseSettings.BaseRoughness;
        float amplitude = 1f;
        float weight = 1f;

        for (int i = 0; i < noiseSettings.NumberOfLayers; i++)
        {
            noise.SetSeed(seed);
            // Invert absolute value by subtracting from 1.
            float v = 1 - Mathf.Abs(noise.GetSimplex(
                point.x * frequency + noiseSettings.Centre.x,
                point.y * frequency + noiseSettings.Centre.y,
                point.z * frequency + noiseSettings.Centre.z));

            // Make ridgis pronounced by squaring v.
            v *= v;

            // Multiply each layer (v), by the weight. Then set the weight to v for the next layer.
            // This way, layers that start lower down will be less affected, and higher layers will be more affected.
            v *= weight;
            weight = Mathf.Clamp01(v * noiseSettings.WeightMultiplier);

            noiseValue += v * amplitude;

            // When Roughness is > 1 the frequency will increase with each layer.
            frequency *= noiseSettings.Roughness;

            // When Persistence is < 1 amplitude will decrease with each layer.
            amplitude *= noiseSettings.Persistence;
        }

        noiseValue = noiseValue - noiseSettings.Minimum;

        return noiseValue * noiseSettings.Strength;
    }
}
