using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class XmlOf
    {
        public static DefDismemberSelf defDismemberSelf;

        public static AbilityDef NzRI_AoJing_FingerNail;


        public static XenoGeneTemplateDef xenoGeneTemplateDef;

        static XmlOf()
        {
            // Log.Message("Initializing XmlOf...");
            DefOfHelper.EnsureInitializedInCtor(typeof(XmlOf));
            // Log.Message("XmlOf initialized.");
        }
    }
}