using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings shapeSettings;
    private INoiseFilter[] noiseFilters;

    [SerializeField]
    private MinMax elevationMinMax;
    public MinMax ElevationMinMax { get { return elevationMinMax; } }

    public void UpdateSettings(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        noiseFilters = new INoiseFilter[shapeSettings.NoiseLayers.Length];

        for( int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(shapeSettings.NoiseLayers[i].NoiseSettings);
        }

        elevationMinMax = new MinMax();
    }
    
    public float CalculateUnscaledElevation(Vector3 pointOnUnitSphere)
    {
        float firstLayer = 0;
        float elevation = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayer = noiseFilters[0].Evaluate(pointOnUnitSphere);

            if (shapeSettings.NoiseLayers[0].Enabled)
            {
                elevation = firstLayer;
            }
        }

        for (int i = 0; i < noiseFilters.Length; i++)
        {
            if (shapeSettings.NoiseLayers[i].Enabled)
            {
                float mask = (shapeSettings.NoiseLayers[i].UseFirstLayerAsMask) ? firstLayer : 1;
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }

        elevationMinMax.Add(elevation);

        return elevation;
    }

    public float GetScaledElevation(float unscaledElevation)
    {
        float elevation = Mathf.Max(0, unscaledElevation);

        elevation = shapeSettings.PlanetRadius * (1 + elevation);

        return elevation;
    }
}
