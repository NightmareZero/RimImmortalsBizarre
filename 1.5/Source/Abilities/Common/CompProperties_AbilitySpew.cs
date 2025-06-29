using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{


    public class CompProperties_AbilitySpew : CompProperties_AbilityEffect
    {
        public float range;

        public float lineWidthEnd;

        public ThingDef filthDef;

        public DamageDef damageType;

        public int damAmount = -1;

        public EffecterDef effecterDef;

        public StatDef damMultiplierStat;

        public bool canHitFilledCells;

        // public CompProperties_AbilitySpew()
        // {
        //     compClass = typeof(CompAbilityEffect_Spew);
        // }
        public override IEnumerable<string> ConfigErrors(AbilityDef parentDef)
        {
            foreach (string error in base.ConfigErrors(parentDef))
            {
                yield return error;
            }
            if (compClass == null)
            {
                yield return parentDef.defName + " has a CompProperties_AbilitySpew but compClass is null.";
            }
        }

    }
}