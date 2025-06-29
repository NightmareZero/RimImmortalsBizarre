using RimWorld;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_FactionFavorability : CompProperties_AbilityEffect
    {
        // 当成功时的思想
        public int onSuccess = 10;

        // 当失败时的思想
        public int onFail = -17;

        public CompProperties_FactionFavorability()
        {
            compClass = typeof(CompAbilityEffect_FactionFavorability);
        }
    }
}