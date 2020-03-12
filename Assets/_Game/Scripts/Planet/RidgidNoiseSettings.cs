using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RidgidNoiseSettings : SimpleNoiseSettings
{
    [SerializeField]
    private float weightMultiplier = 0.8f;
    public float WeightMultiplier { get { return weightMultiplier; } }
}
