using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{
    // CompProperties_AbilitySprayLiquid


    public class CompProperties_AbilitySpray : CompProperties_AbilityEffect
    {

        public ThingDef projectileDef;

        public int numCellsToHit;

        public EffecterDef sprayEffecter;

        public CompProperties_AbilitySpray()
        {
            compClass = typeof(CompAbilityEffect_Spray);
        }
    }
}