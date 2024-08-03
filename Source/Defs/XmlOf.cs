using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class XmlOf
    {
        public static DefPartHurt defPartHurt;

        public static AbilityDef NzRI_AoJing_FingerNail;

        static XmlOf()
        {
            // Log.Message("Initializing XmlOf...");
            DefOfHelper.EnsureInitializedInCtor(typeof(XmlOf));
            // Log.Message("XmlOf initialized.");
        }
    }
}