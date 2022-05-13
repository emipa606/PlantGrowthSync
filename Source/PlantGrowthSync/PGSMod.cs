using System;
using UnityEngine;
using Verse;

namespace PlantGrowthSync;

public class PGSMod : Mod
{
    public Vector2 scrollPosition;
    private PGSModSettings settings;

    public PGSMod(ModContentPack con) : base(con)
    {
        settings = GetSettings<PGSModSettings>();
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
        var lister = new Listing_Standard();

        var height = canvas.y + 240f; // set height here
        var viewRect = new Rect(0f, 0f, canvas.width - 260f, height);


        lister.Begin(canvas);
        Widgets.BeginScrollView(new Rect(120f, 0f, canvas.width - 240f, canvas.height), ref scrollPosition, viewRect);
        lister.Settings_Header("PGSHeader".Translate(), Color.clear);
        lister.GapLine();
        lister.Gap();

        string dayExplain = " " + "SecondsExplain".Translate() + " (" +
                            (PGSModSettings.TimeBetweenChecks / 60 / 15).ToString() + " " +
                            "Speed3Explain".Translate() + ")";
        var timeChecks = AddToLabel("TimeBetweenChecks".Translate(), PGSModSettings.TimeBetweenChecks, divideBy: 60,
            postLabel: dayExplain);
        lister.Settings_SliderLabeled("SyncRatePerFullGrowth".Translate(), "%",
            ref PGSModSettings.SyncRatePerFullGrowth, 0f, 0.5f, roundNumber: RoundFloat.Hundredth);
        lister.Settings_SliderLabeled(timeChecks, string.Empty, ref PGSModSettings.TimeBetweenChecks, 600, 3600,
            roundNumber: RoundFloat.Tenth);
        lister.End();
        Widgets.EndScrollView();

        base.DoSettingsWindowContents(canvas);
    }

    public override string SettingsCategory()
    {
        return "ModTitle".Translate();
    }

    public string AddFlooredLabel(string label, float number, float multiplier, string preLabel = null,
        string postLabel = null)
    {
        if (preLabel == null)
        {
            preLabel = "";
        }

        if (postLabel == null)
        {
            postLabel = "";
        }


        return label + ": " + preLabel + Math.Floor(number * multiplier) + postLabel;
    }

    public string AddToLabel(string label, float number, float multiplier = -1f, float divideBy = -1f,
        string preLabel = null, string postLabel = null)
    {
        if (preLabel == null)
        {
            preLabel = "";
        }

        if (postLabel == null)
        {
            postLabel = "";
        }

        if (multiplier > 0f)
        {
            return label + ": " + preLabel + (number * multiplier) + postLabel;
        }

        if (divideBy > 0f)
        {
            return label + ": " + preLabel + (number / divideBy) + postLabel;
        }

        return label + ": " + preLabel + number + postLabel;
    }
}