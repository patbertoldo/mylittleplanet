﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NoiseFilterFactory
{
    public static INoiseFilter CreateNoiseFilter(NoiseSettings noiseSettings)
    {
        switch(noiseSettings.FilterType)
        {
            case FilterType.Simple:
                return new SimpleNoiseFilter(noiseSettings.SimpleNoiseSettings);
            case FilterType.Ridgid:
                return new RidgidNoiseFilter(noiseSettings.RidgidNoiseSettings);
        }

        return null;
    }
}
