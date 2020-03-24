using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    [SerializeField]
    private BiomeColorSettings biomeColorSettings;
    public BiomeColorSettings BiomeColorSettings { get { return biomeColorSettings; } }

    [SerializeField]
    private Material planetMaterial;
    public Material PlanetMaterial { get { return planetMaterial; } }

    [SerializeField]
    private Gradient oceanColor;
    public Gradient OceanColor { get { return oceanColor; } }
}
