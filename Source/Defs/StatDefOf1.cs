using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static partial class StatDefOf1
    {
        // 袄景教技能威力系数
        public static StatDef NzRI_AoJingPowerMultiplier;

        // 正德寺技能威力系数
        public static StatDef NzRI_ZhengDePowerMultiplier;

        public static void Init()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf1));
        }
    }
}