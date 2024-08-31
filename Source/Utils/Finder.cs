using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    { 
        public static Hediff GetAscendHediff(Pawn pawn)
        {
            return pawn.health.hediffSet.GetFirstHediffOfDef(XmlOf.NzRI_AscendHediff);
        }

        public static bool HasAscendHediff(Pawn pawn)
        {
            return GetAscendHediff(pawn) != null;
        }
    }
}