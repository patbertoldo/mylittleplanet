using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FilterType
{
    Simple,
    Ridgid
}

[System.Serializable]
public class NoiseSettings
{
    [SerializeField]
    private FilterType filterType;
    public FilterType FilterType { get { return filterType; } }

    [SerializeField, ConditionalHide("filterType", 0)]
    private SimpleNoiseSettings simpleNoiseSettings;
    public SimpleNoiseSettings SimpleNoiseSettings { get { return simpleNoiseSettings; } }

    [SerializeField, ConditionalHide("filterType", 1)]
    private RidgidNoiseSettings ridgidNoiseSettings;
    public RidgidNoiseSettings RidgidNoiseSettings { get { return ridgidNoiseSettings; } }
}
