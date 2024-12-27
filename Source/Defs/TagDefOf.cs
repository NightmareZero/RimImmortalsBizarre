using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class TagDefOf
    {
        // 心素身体部件
        public static TagDef NzRI_XinSuBodyPart;

        // 正德寺 身体部件
        public static TagDef NzRI_ZhengDe_BodyPart;

        // 正德寺 克己
        public static TagDef NzRI_ZhengDe_Restraint;

        // 正德寺 明心
        public static TagDef NzRI_ZhengDe_Enlightenment;

        // 正德寺 舍身
        public static TagDef NzRI_ZhengDe_Sacrifice;

        static TagDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(TagDefOf));
        }
    }
}