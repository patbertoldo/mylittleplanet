using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings shapeSettings;
    private NoiseFilter[] noiseFilters;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        this.shapeSettings = shapeSettings;
        noiseFilters = new NoiseFilter[shapeSettings.NoiseLayers.Length];

        for( int i = 0; i < noiseFilters.Length; i++)
        {
            noiseFilters[i] = new NoiseFilter(shapeSettings.NoiseLayers[i].NoiseSettings);
        }
    }
    
    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
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
        return pointOnUnitSphere * shapeSettings.PlanetRadius * (elevation + 1);
    }
}
