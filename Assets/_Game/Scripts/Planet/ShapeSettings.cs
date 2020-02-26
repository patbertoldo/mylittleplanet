using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    [SerializeField]
    private float planetRadius = 1;
    public float PlanetRadius { get { return planetRadius; } }

}
