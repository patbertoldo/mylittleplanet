using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    [SerializeField]
    private float strength = 1;
    public float Strength { get { return strength; } }

    [SerializeField, Range(1, 8)]
    private int numberOfLayers = 1;
    public int NumberOfLayers { get { return numberOfLayers; } }

    [SerializeField]
    private float baseRoughness = 1;
    public float BaseRoughness { get { return baseRoughness; } }

    [SerializeField]
    private float roughness = 2;
    public float Roughness { get { return roughness; } }

    [SerializeField]
    private float persistence = 0.5f;
    public float Persistence { get { return persistence; } }

    [SerializeField]
    private Vector3 centre;
    public Vector3 Centre { get { return centre; } }

    [SerializeField]
    private float minimum;
    public float Minimum { get { return minimum; } }
}
