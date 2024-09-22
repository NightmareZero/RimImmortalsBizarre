using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class ThingOf {

        // 类别: 附件
        public static ThingCategoryDef ApparelUtility;

        // 类别: 武器
        public static ThingCategoryDef Weapons;

        // 类别: 衣服
        public static ThingCategoryDef Apparel;

        // 类别: 一切
        public static ThingCategoryDef Root;

        static ThingOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(ThingOf));
        }
    }
}