using RimWorld;
using System.Linq;



namespace NzRimImmortalBizarre
{ 
    public class CompProperties_GoodImpression : CompProperties_AbilityEffect
    {
        // 当成功时的思想
        public ThoughtDef onSuccess = XmlOf.NzRI_SaveMe;

        // 当失败时的思想
        public ThoughtDef onFail = XmlOf.NzRI_CheatMe;


        public CompProperties_GoodImpression()
        {
            compClass = typeof(CompAbilityEffect_GoodImpression);
        }
    }
}