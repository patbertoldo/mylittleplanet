using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    [SerializeField]
    private float planetRadius = 1;
    public float PlanetRadius { get { return planetRadius; } }

    [SerializeField]
    private NoiseLayer[] noiseLayers;
    public NoiseLayer[] NoiseLayers { get { return noiseLayers; } }

    private int seed = 1337;
    public int Seed { get { return seed; } set { seed = value; } }

    [System.Serializable]
    public class NoiseLayer
    {
        [SerializeField]
        private bool enabled = true;
        public bool Enabled { get { return enabled; } }

        [SerializeField]
        private bool useFirstLayerAsMask;
        public bool UseFirstLayerAsMask { get { return useFirstLayerAsMask; } }

        [SerializeField]
        private NoiseSettings noiseSettings;
        public NoiseSettings NoiseSettings { get { return noiseSettings; } }
    }
}
