using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    
    public class CompProperties_DivineLightKick : CompProperties_AbilitySpew
    {
        public float stunTime = 6;

        public bool stunBoss = false;

        public CompProperties_DivineLightKick()
        {
            compClass = typeof(CompAbilityEffect_DivineLightKick);
        }
    }
}