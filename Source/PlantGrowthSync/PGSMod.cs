using System;
using Mlie;
using UnityEngine;
using Verse;

namespace PlantGrowthSync;

public class PGSMod : Mod
{
    private static string currentVersion;

    public PGSMod(ModContentPack con) : base(con)
    {
        GetSettings<PGSModSettings>();
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(con.ModMetaData);
    }

    public override void DoSettingsWindowContents(Rect canvas)
    {
        var lister = new Listing_Standard();

        lister.Begin(canvas);
        lister.GapLine();
        lister.Gap();

        string dayExplain = " " + "SecondsExplain".Translate() + " (" +
                            (PGSModSettings.TimeBetweenChecks / 60 / 15).ToString() + " " +
                            "Speed3Explain".Translate() + ")";
        var timeChecks = AddToLabel("TimeBetweenChecks".Translate(), PGSModSettings.TimeBetweenChecks, divideBy: 60,
            postLabel: dayExplain);
        lister.Settings_SliderLabeled(
            "SyncRatePerFullGrowth".Translate(), "%",
            ref PGSModSettings.SyncRatePerFullGrowth, 0f, 0.5f, roundNumber: RoundFloat.Hundredth);
        lister.Settings_SliderLabeled(timeChecks, string.Empty, ref PGSModSettings.TimeBetweenChecks, 600, 3600,
            roundNumber: RoundFloat.Tenth);
        if (currentVersion != null)
        {
            lister.Gap();
            GUI.contentColor = Color.gray;
            lister.Label("GrowthSyncCurrentModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        lister.End();

        base.DoSettingsWindowContents(canvas);
    }

    public override string SettingsCategory()
    {
        return "ModTitle".Translate();
    }

    public string AddFlooredLabel(string label, float number, float multiplier, string preLabel = null,
        string postLabel = null)
    {
        preLabel ??= "";

        postLabel ??= "";


        return $"{label}: {preLabel}{Math.Floor(number * multiplier)}{postLabel}";
    }

    private string AddToLabel(string label, float number, float multiplier = -1f, float divideBy = -1f,
        string preLabel = null, string postLabel = null)
    {
        preLabel ??= "";

        postLabel ??= "";

        if (multiplier > 0f)
        {
            return $"{label}: {preLabel}{number * multiplier}{postLabel}";
        }

        return divideBy > 0f
            ? $"{label}: {preLabel}{number / divideBy}{postLabel}"
            : $"{label}: {preLabel}{number}{postLabel}";
    }
}