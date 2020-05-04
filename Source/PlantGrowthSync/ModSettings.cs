using System;
using UnityEngine;
using Verse;

namespace PlantGrowthSync
{
  public class PGSModSettings : ModSettings
  {
    public static float SyncRatePerFullGrowth = 0.150000005960464f;
    public static int TimeBetweenChecks = 2000;

    public override void ExposeData()
    {
      base.ExposeData();
      Scribe_Values.Look(ref SyncRatePerFullGrowth, "SyncRatePerFullGrowth");
      Scribe_Values.Look(ref TimeBetweenChecks, "TimeBetweenChecks");
    }
  }

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
      Listing_Standard lister = new Listing_Standard();

      float height = canvas.y + 240f; // set height here
      Rect viewRect = new Rect(0f, 0f, canvas.width - 260f, height);


      lister.Begin(canvas);
      lister.BeginScrollView(new Rect(120f, 0f, canvas.width - 240f, canvas.height), ref scrollPosition, ref viewRect);
      lister.Settings_Header("PGSHeader".Translate(), Color.clear, GameFont.Medium, TextAnchor.MiddleLeft);
      lister.GapLine();
      lister.Gap(12f);

      string dayExplain = " " + "SecondsExplain".Translate() + " (" + (PGSModSettings.TimeBetweenChecks / 60 / 15).ToString() + " " + "Speed3Explain".Translate() + ")";
      string timeChecks = AddToLabel("TimeBetweenChecks".Translate(), PGSModSettings.TimeBetweenChecks, divideBy: 60, postLabel: dayExplain);
      lister.Settings_SliderLabeled("SyncRatePerFullGrowth".Translate(), "%", ref PGSModSettings.SyncRatePerFullGrowth, 0f, 0.5f, roundNumber: RoundFloat.Hundredth);
      lister.Settings_SliderLabeled(timeChecks, string.Empty, ref PGSModSettings.TimeBetweenChecks, 600, 3600, roundNumber: RoundFloat.Tenth);
      lister.End();
      lister.EndScrollView(ref viewRect);

      base.DoSettingsWindowContents(canvas);
    }

    public override string SettingsCategory()
    {
      return "ModTitle".Translate();
    }

    public string AddFlooredLabel(string label, float number, float multiplier, string preLabel = null, string postLabel = null)
    {
      if (preLabel == null)
        preLabel = "";

      if (postLabel == null)
        postLabel = "";


      return label + ": " + preLabel + Math.Floor(number * multiplier).ToString() + postLabel;
    }

    public string AddToLabel(string label, float number, float multiplier = -1f, float divideBy = -1f, string preLabel = null, string postLabel = null)
    {
      if (preLabel == null)
        preLabel = "";

      if (postLabel == null)
        postLabel = "";

      if (multiplier > 0f)
      {
        return label + ": " + preLabel + (number * multiplier).ToString() + postLabel;
      } else if (divideBy > 0f)
      {
        return label + ": " + preLabel + (number / divideBy).ToString() + postLabel;
      } else
      {
        return label + ": " + preLabel + (number).ToString() + postLabel;
      }
    }
  }
}
