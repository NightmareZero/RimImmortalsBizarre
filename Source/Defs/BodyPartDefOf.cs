using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class BodyPartDef1Of {

        public static HediffDef NzRI_XinSu_Eye;
        static BodyPartDef1Of()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(BodyPartDef1Of));
        }
    }
}