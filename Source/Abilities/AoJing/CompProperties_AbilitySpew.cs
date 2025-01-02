using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    
    public class CompProperties_AJ_AbilitySpew : CompProperties_AbilitySpew
    {
        // 技能教派
        public int skillRoute = 0;

        public CompProperties_AJ_AbilitySpew()
        {
            compClass = typeof(CompAbilityEffect_Spew);
        }
    }
}