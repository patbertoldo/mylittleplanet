using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    private ColorSettings colorSettings;
    private Texture2D texture;
    private const int textureResolution = 50;
    private INoiseFilter biomeNoiseFilter;

    public void UpdateSettings(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;

        if (texture == null || texture.height != colorSettings.BiomeColorSettings.Biomes.Length)
        {
            texture = new Texture2D(textureResolution, colorSettings.BiomeColorSettings.Biomes.Length, TextureFormat.RGBA32, false);
        }

        biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(colorSettings.BiomeColorSettings.NoiseSettings);
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        colorSettings.PlanetMaterial.SetVector("_elevationMinMax", new Vector3(elevationMinMax.Min, elevationMinMax.Max));
    }

    public float BiomePercentFromPoint(Vector3 pointOnUnitSphere)
    {
        // 0 is the planet's south pole, 1 is the north pole.
        // Unit sphere will be -1 to +1 
        float heightPercent = (pointOnUnitSphere.y + 1) / 2f;

        // Use noise to shift biome borders.
        heightPercent += (biomeNoiseFilter.Evaluate(pointOnUnitSphere) - colorSettings.BiomeColorSettings.NoiseOffset) * colorSettings.BiomeColorSettings.NoiseStrength;

        float biomeIndex = 0;
        int biomesTotal = colorSettings.BiomeColorSettings.Biomes.Length;

        // Blend won't work well with a zero value, add small amount to the end.
        float blendRange = colorSettings.BiomeColorSettings.BlendAmount / 2f + 0.001f;

        for (int i = 0; i < biomesTotal; i++)
        {
            float distance = heightPercent - colorSettings.BiomeColorSettings.Biomes[i].StartHeight;
            float weight = Mathf.InverseLerp(-blendRange, blendRange, distance);

            // Weight of 1 will keep the previous biome color. In that case, reset the biome index.
            biomeIndex *= (1 - weight);

            // Add on current biome index.
            biomeIndex += i * weight;
        }

        // Max so there is no divide by zero. Return value between 0 and 1.
        return biomeIndex / Mathf.Max(1, biomesTotal - 1);
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[texture.width * texture.height];

        int colorIndex = 0;

        foreach (var biome in colorSettings.BiomeColorSettings.Biomes)
        {
            for (int i = 0; i < textureResolution; i++)
            {
                Color gradientColor = biome.Gradient.Evaluate(i / (textureResolution - 1f));
                Color tintColor = biome.Tint;

                colors[colorIndex] = gradientColor * (1 - biome.TintPercent) + tintColor * biome.TintPercent;
                colorIndex++;
            }
        }

        texture.SetPixels(colors);
        texture.Apply();
        colorSettings.PlanetMaterial.SetTexture("_texture", texture);
    }
}
