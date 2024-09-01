using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    {
        public static Hediff GetAscendHediff(Pawn pawn)
        {
            return pawn.health.hediffSet.GetFirstHediffOfDef(XmlOf.NzRI_AoJing_Ascend);
        }

        public static bool HasAscendHediff(Pawn pawn)
        {
            return GetAscendHediff(pawn) != null;
        }

        public static List<Pawn> GetPawnsInAffectedArea(Map map,List<IntVec3> affectedCells)
        {
            List<Pawn> pawnsInAffectedArea = new List<Pawn>();
            foreach (IntVec3 cell in affectedCells)
            {
                foreach (Thing thing in cell.GetThingList(map))
                {
                    if (thing is Pawn pawn)
                    {
                        pawnsInAffectedArea.Add(pawn);
                    }
                }
            }
            return pawnsInAffectedArea;
        }

        public static int EveryPawnInAffectedArea(Map map, List<IntVec3> affectedCells, System.Action<Pawn> action)
        {
            int count = 0;
            foreach (IntVec3 cell in affectedCells)
            {
                foreach (Thing thing in cell.GetThingList(map))
                {
                    if (thing is Pawn pawn)
                    {
                        action(pawn);
                        count++;
                    }
                }
            }
            return count;
        }
    }
}