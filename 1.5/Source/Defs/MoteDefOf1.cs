using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class MoteDefOf
    {
        // public static MoteDef Mote_Rope;

        static MoteDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(MoteDefOf));
        }
    }
}