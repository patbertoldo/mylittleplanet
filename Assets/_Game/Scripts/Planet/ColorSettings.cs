using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    [SerializeField]
    private Gradient gradient;
    public Gradient Gradient { get { return gradient; } }

    [SerializeField]
    private Material planetMaterial;
    public Material PlanetMaterial { get { return planetMaterial; } }
}
