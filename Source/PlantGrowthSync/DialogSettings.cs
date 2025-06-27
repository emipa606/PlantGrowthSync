using System;
using Verse;

namespace PlantGrowthSync;

[StaticConstructorOnStartup]
public static class DialogSettings
{
    private static float roundToNearestHalf(float val)
    {
        return (float)Math.Round(val * 2, MidpointRounding.AwayFromZero) / 2;
    }

    private static float roundToNearestTenth(float val)
    {
        return (float)Math.Round(val * 10, MidpointRounding.AwayFromZero) / 10;
    }

    private static float roundToNearestHundredth(float val)
    {
        return (float)Math.Round(val * 100, MidpointRounding.AwayFromZero) / 100;
    }

    public static void Settings_SliderLabeled(this Listing_Standard lister, string label, string endSymbol,
        ref float value, float min, float max, float multiplier = 1f, int decimalPlaces = 2, float hardMaximum = -1f,
        string valueDisplayWhenMax = "", RoundFloat roundNumber = RoundFloat.Hundredth)
    {
        lister.Gap();
        Text.Font = GameFont.Small;
        var rect = lister.GetRect(24f);
        var labelValue = string.Format("{0}" + endSymbol, Math.Round(value * multiplier, decimalPlaces) * 100);

        if (endSymbol == "%")
        {
            labelValue = string.Format("{0}" + endSymbol, value * 100);
        }

        if (!valueDisplayWhenMax.NullOrEmpty() &&
            hardMaximum > 0) // when value is max, show a string instead (infinite, max, etc.)
        {
            if (value >= hardMaximum)
            {
                labelValue = valueDisplayWhenMax;
            }
        }

        switch (roundNumber)
        {
            case RoundFloat.Tenth:
                value = Widgets.HorizontalSlider(rect, roundToNearestTenth(value), min, max, false, null, label,
                    labelValue);
                break;
            case RoundFloat.Half:
                value = Widgets.HorizontalSlider(rect, roundToNearestHalf(value), min, max, false, null, label,
                    labelValue);
                break;
            case RoundFloat.Hundredth:
                value = Widgets.HorizontalSlider(rect, roundToNearestHundredth(value), min, max, false, null, label,
                    labelValue);
                break;
            case RoundFloat.None:
                value = Widgets.HorizontalSlider(rect, value, min, max, false, null, label, labelValue);
                break;
        }

        if (hardMaximum > 0 && value >= max)
        {
            value = hardMaximum;
        }
    }

    public static void Settings_SliderLabeled(this Listing_Standard lister, string label, string endSymbol,
        ref int value, int min, int max, int hardMaximum = -1, string valueDisplayWhenMax = "",
        RoundFloat roundNumber = RoundFloat.None)
    {
        lister.Gap();
        var rect = lister.GetRect(24f);
        var labelValue = $"{value}{endSymbol}";
        if (!valueDisplayWhenMax.NullOrEmpty() &&
            hardMaximum > 0) // when value is max, show a string instead (infinite, max, etc.)
        {
            if (value == hardMaximum)
            {
                labelValue = valueDisplayWhenMax;
            }
        }

        value = (int)Widgets.HorizontalSlider(rect, value, min, max, false, null, label, labelValue);

        switch (roundNumber)
        {
            case RoundFloat.Tenth:
                value = (int)Widgets.HorizontalSlider(rect, roundToNearestTenth(value), min, max, false, null, label,
                    labelValue);
                break;
            case RoundFloat.Half:
                value = (int)Widgets.HorizontalSlider(rect, roundToNearestHalf(value), min, max, false, null, label,
                    labelValue);
                break;
            case RoundFloat.Hundredth:
                value = (int)Widgets.HorizontalSlider(rect, roundToNearestHundredth(value), min, max, false, null,
                    label,
                    labelValue);
                break;
            case RoundFloat.None:
                value = (int)Widgets.HorizontalSlider(rect, value, min, max, false, null, label, labelValue);
                break;
        }

        if (hardMaximum > 0 && value == max)
        {
            value = hardMaximum;
        }
    }
}