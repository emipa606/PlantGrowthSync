using Verse;

namespace PlantGrowthSync;

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