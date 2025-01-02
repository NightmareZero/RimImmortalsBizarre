using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static partial class StatDefOf1
    {
        public static StatDef NzRI_AoJingPowerMultiplier;

        public static void Init()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(StatDefOf1));
        }
    }
}