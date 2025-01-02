using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    
    public class CompProperties_DivineLightKick : CompProperties_AbilitySpew
    {
        public float stuckTime = 5;

        public CompProperties_DivineLightKick()
        {
            compClass = typeof(CompAbilityEffect_DivineLightKick);
        }
    }
}