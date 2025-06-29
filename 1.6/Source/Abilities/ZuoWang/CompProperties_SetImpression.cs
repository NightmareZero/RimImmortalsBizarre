using RimWorld;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_SetImpression : CompProperties_AbilityEffect
    {
        // 当成功时的思想
        public ThoughtDef onSuccess;

        // 当失败时的思想
        public ThoughtDef onFail;


        public CompProperties_SetImpression()
        {
            compClass = typeof(CompAbilityEffect_SetImpression);
        }
    }
}