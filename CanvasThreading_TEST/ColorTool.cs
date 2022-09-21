using System;
using System.Windows.Media;

namespace CanvasThreading_TEST;

internal static class ColorTool
{
    static readonly Lazy<Random> random = new();

    internal static SolidColorBrush RandomColor => 
        new(Color.FromRgb((byte)random.Value.Next(0, 256),
                          (byte)random.Value.Next(0, 256),
                          (byte)random.Value.Next(0, 256)));

    internal static SolidColorBrush GetMultipliedColor(int shadeIndex, int redMultiplier, int greenMultiplier, int blueMultiplier)
    {
        shadeIndex *= 8;

        return new(Color.FromRgb((byte)(redMultiplier * shadeIndex),
                                 (byte)(greenMultiplier * shadeIndex),
                                 (byte)(blueMultiplier * shadeIndex)));
    }

    internal static SolidColorBrush GetShadedColor(ColorShadeType type, int shadeIndex)
    {
        return type switch
        {
            ColorShadeType.Red =>
                GetRedShadeColor(shadeIndex),

            ColorShadeType.Green =>
                GetGreenShadeColor(shadeIndex),

            ColorShadeType.Blue =>
                GetBlueShadeColor(shadeIndex),

            ColorShadeType.Grey => 
                GetGreyShadeColor(shadeIndex),

             _ => null!
        };
    }

    internal static SolidColorBrush GetGreyShadeColor(int shadeIndex)
    {
        shadeIndex *= 8;

        byte shade = (byte)shadeIndex;

        return new(Color.FromRgb(shade, shade, shade));
    }

    internal static SolidColorBrush GetRedShadeColor(int shadeIndex)
    {
        shadeIndex *= 8;

        byte shade = (byte)shadeIndex;

        return new(Color.FromRgb(shade, 0, 0));
    }

    internal static SolidColorBrush GetGreenShadeColor(int shadeIndex)
    {
        shadeIndex *= 8;

        byte shade = (byte)shadeIndex;

        return new(Color.FromRgb(0, shade, 0));
    }

    internal static SolidColorBrush GetBlueShadeColor(int shadeIndex)
    {
        shadeIndex *= 8;

        byte shade = (byte)shadeIndex;

        return new(Color.FromRgb(0, 0, shade));
    }
}

public enum ColorShadeType
{
    Red, Green, Blue, Grey
}