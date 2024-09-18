using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    // WhoXiuXian.Abilities.CompProperties__ReduceEnergy

    public class CompProperties_Zw_AddFg : CompProperties_AbilityEffect,IsCastingFailable
    {
        public int change;

        public bool enough = true;

        public CompProperties_Zw_AddFg()
        {
            compClass = typeof(CompAbilityEffect_Zw_AddFg);
        }

        public bool IsCastingSuccess()
        {
           return enough;
        }

    }

}