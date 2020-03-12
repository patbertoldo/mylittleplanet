using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator
{
    private ColorSettings colorSettings;
    private Texture2D texture;
    private const int textureResolution = 50;

    public void UpdateSettings(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;

        if (texture == null)
        {
            texture = new Texture2D(textureResolution, 1);
        }
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        colorSettings.PlanetMaterial.SetVector("_elevationMinMax", new Vector3(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColors()
    {
        Color[] colors = new Color[textureResolution];

        for (int i = 0; i < textureResolution; i++)
        {
            colors[i] = colorSettings.Gradient.Evaluate(i / (textureResolution - 1f));
        }

        texture.SetPixels(colors);
        texture.Apply();
        colorSettings.PlanetMaterial.SetTexture("_texture", texture);
    }
}
