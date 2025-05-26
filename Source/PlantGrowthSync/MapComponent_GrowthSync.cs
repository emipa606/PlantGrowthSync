using System;
using System.Collections.Generic;
using RimWorld;
using Verse;


// Thank you to Lanilor for writing the basis of this file!


namespace PlantGrowthSync;

public class MapComponent_GrowthSync(Map map) : MapComponent(map)
{
    private TickManager tickMan;

    public override void FinalizeInit()
    {
        tickMan = Find.TickManager;

        base.FinalizeInit();
    }

    public override void MapComponentTick()
    {
        if (tickMan.TicksGame % PGSModSettings.TimeBetweenChecks != 0) // 60 ticks = 1 second
        {
            return;
        }

        foreach (var zone in map.zoneManager.AllZones)
        {
            if (zone.GetType().GetInterface("IPlantToGrowSettable") == null)
            {
                continue;
            }

            var interfaceZone = (IPlantToGrowSettable)zone;
            var growRate = (float)(PGSModSettings.SyncRatePerFullGrowth /
                                   (interfaceZone.GetPlantDefToGrow().plant.growDays * 30.0));
            var allContainedThings = zone.AllContainedThings;
            var plantList = new List<Plant>();
            var totalPlantGrowth = 0.0f;

            foreach (var thing in allContainedThings)
            {
                if (thing.GetType() != typeof(Plant) && !thing.GetType().IsSubclassOf(typeof(Plant)))
                {
                    continue;
                }

                var plant = (Plant)thing;
                if (!plant.IsCrop || plant.LifeStage != PlantLifeStage.Growing || plant.GrowthRate <= 0)
                {
                    continue;
                }

                totalPlantGrowth += plant.Growth;
                plantList.Add(plant);
            }

            var averagePlantGrowth = (float)Math.Round(totalPlantGrowth / plantList.Count, 4);
            var underAveragePlants = 0;
            var overAveragePlants = 0;

            for (var index = plantList.Count - 1; index >= 0; --index)
            {
                if (Math.Abs(averagePlantGrowth - plantList[index].Growth) <= (double)growRate)
                {
                    plantList[index].Growth = averagePlantGrowth;
                    plantList.RemoveAt(index);
                }
                else
                {
                    if (plantList[index].Growth < averagePlantGrowth)
                    {
                        ++underAveragePlants;
                    }

                    if (plantList[index].Growth > averagePlantGrowth)
                    {
                        ++overAveragePlants;
                    }
                }
            }

            var moreUnderThanOver = 1f;
            var moreOverThanUnder = 1f;

            if (underAveragePlants > 0 && overAveragePlants > 0)
            {
                if (underAveragePlants > overAveragePlants)
                {
                    moreUnderThanOver = (float)overAveragePlants / underAveragePlants;
                }

                else
                {
                    moreOverThanUnder = (float)underAveragePlants / overAveragePlants;
                }
            }

            foreach (var plant in plantList)
            {
                if (plant.Growth < averagePlantGrowth)
                {
                    plant.Growth += growRate * moreUnderThanOver;
                }

                if (!(plant.Growth > averagePlantGrowth))
                {
                    continue;
                }

                if (plant.Growth - (growRate * moreOverThanUnder) < averagePlantGrowth)
                {
                    plant.Growth = averagePlantGrowth;
                }
                else
                {
                    plant.Growth -= growRate * moreOverThanUnder;
                }
            }
        }
    }
}