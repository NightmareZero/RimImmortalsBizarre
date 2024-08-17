using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class CompProperties_Regeneration : CompProperties_AbilityEffect { 

        // 恢复次数
        public int times;

        public CompProperties_Regeneration()
        {
            compClass = typeof(CompAbilityEffect_Regeneration);
        }
    }
}