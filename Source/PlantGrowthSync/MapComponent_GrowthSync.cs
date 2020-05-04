using RimWorld;
using System;
using System.Collections.Generic;
using Verse;


// Thank you to Lanilor for writing the basis of this file!


namespace PlantGrowthSync
{
  public class MapComponent_GrowthSync : MapComponent
  {
    public TickManager tickMan;

    public MapComponent_GrowthSync(Map map) : base(map)
    {
    
    }

    public override void FinalizeInit()
    {
      tickMan = Find.TickManager;

      base.FinalizeInit();
    }

    public override void MapComponentTick()
    {
      if (tickMan.TicksGame % PGSModSettings.TimeBetweenChecks != 0) // 60 ticks = 1 second
        return;

      foreach (Zone allZone in map.zoneManager.AllZones)
      {
        if (allZone.GetType() == typeof(Zone_Growing))
        {
          Zone_Growing zoneGrowing = (Zone_Growing)allZone;
          float growRate = (float)(PGSModSettings.SyncRatePerFullGrowth / (zoneGrowing.GetPlantDefToGrow().plant.growDays * 30.0));
          IEnumerable<Thing> allContainedThings = zoneGrowing.AllContainedThings;
          List<Plant> plantList = new List<Plant>();
          float totalPlantGrowth = 0.0f;

          foreach (Thing thing in allContainedThings)
          {
            if (thing.GetType() == typeof(Plant))
            {
              Plant plant = (Plant)thing;
              if (plant.IsCrop && plant.LifeStage == PlantLifeStage.Growing)
              {
                totalPlantGrowth += plant.Growth;
                plantList.Add(plant);
              }
            }
          }

          float averagePlantGrowth = totalPlantGrowth / plantList.Count;
          int underAveragePlants = 0;
          int overAveragePlants = 0;

          for (int index = plantList.Count - 1; index >= 0; --index)
          {
            if (Math.Abs(averagePlantGrowth - plantList[index].Growth) <= (double)growRate)
            {
              plantList[index].Growth = averagePlantGrowth;
              plantList.RemoveAt(index);
            }
            else
            {
              if (plantList[index].Growth < averagePlantGrowth)
                ++underAveragePlants;

              if (plantList[index].Growth > averagePlantGrowth)
                ++overAveragePlants;
            }
          }

          float moreUnderThanOver = 1f;
          float moreOverThanUnder = 1f;

          if (underAveragePlants > 0 && overAveragePlants > 0)
          {
            if (underAveragePlants > overAveragePlants)
              moreUnderThanOver = overAveragePlants / underAveragePlants;

            else
              moreOverThanUnder = underAveragePlants / overAveragePlants;
          }

          foreach (Plant plant in plantList)
          {
            if (plant.Growth < averagePlantGrowth)
              plant.Growth += growRate * moreUnderThanOver;

            if (plant.Growth > averagePlantGrowth)
              plant.Growth -= growRate * moreOverThanUnder;
          }
        }
      }
    }
  }
}
