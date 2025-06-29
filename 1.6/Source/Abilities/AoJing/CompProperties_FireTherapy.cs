using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{
    // CompProperties_AbilityCoagulate

    public class CompProperties_FireTherapy : CompProperties_AbilityEffect
    {
        public FloatRange tendQualityRange;

        public CompProperties_FireTherapy()
        {
            compClass = typeof(CompAbilityEffect_FireTherapy);
        }
    }
    
}