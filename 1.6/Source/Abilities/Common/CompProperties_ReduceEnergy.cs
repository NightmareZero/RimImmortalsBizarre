using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_ReduceEnergy : CompProperties_AbilityEffect
    {
        public int rEnergy;

        public CompProperties_ReduceEnergy()
        {
            compClass = typeof(CompAbilityEffect_ReduceEnergy);
        }
    }
}