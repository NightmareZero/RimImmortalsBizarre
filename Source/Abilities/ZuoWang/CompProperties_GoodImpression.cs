using RimWorld;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_GoodImpression : CompProperties_AbilityEffect
    {
        // 当成功时的思想
        public ThoughtDef onSuccess;

        // 当失败时的思想
        public ThoughtDef onFail;


        public CompProperties_GoodImpression()
        {
            compClass = typeof(CompAbilityEffect_GoodImpression);
        }
    }
}