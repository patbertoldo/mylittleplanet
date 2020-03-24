using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomeColorSettings
{
    [SerializeField]
    private Biome[] biomes;
    public Biome[] Biomes { get { return biomes; } }

    [SerializeField]
    private NoiseSettings noiseSettings;
    public NoiseSettings NoiseSettings { get { return noiseSettings; } }

    [SerializeField]
    private float noiseOffset;
    public float NoiseOffset { get { return noiseOffset; } }

    [SerializeField]
    private float noiseStrength;
    public float NoiseStrength { get { return noiseStrength; } }

    [SerializeField, Range(0,1)]
    private float blendAmount;
    public float BlendAmount { get { return blendAmount; } }

    [System.Serializable]
    public class Biome
    {
        [SerializeField]
        private Gradient gradient;
        public Gradient Gradient { get { return gradient; } }

        [SerializeField]
        private Color tint;
        public Color Tint { get { return tint; } }

        [SerializeField, Range(0,1)]
        private float tintPercent;
        public float TintPercent { get { return tintPercent; } }

        [SerializeField, Range(0, 1)]
        private float startHeight;
        public float StartHeight { get { return startHeight; } }
    }
}