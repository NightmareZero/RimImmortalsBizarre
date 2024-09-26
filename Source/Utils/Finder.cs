using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    {
        /// <summary>
        /// 获取登阶Hediff
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Hediff GetAscendHediff(Pawn pawn)
        {
            return pawn.health.hediffSet.GetFirstHediffOfDef(XmlOf.NzRI_AoJing_Ascend);
        }

        /// <summary>
        /// 是否有登阶状态
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static bool HasAscendHediff(Pawn pawn)
        {
            return GetAscendHediff(pawn) != null;
        }

        /// <summary>
        /// 获取非罡Hediff
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Zw_Fg GetFeiGang(Pawn pawn)
        {
            return (Zw_Fg)pawn.health.hediffSet.GetFirstHediffOfDef(XmlOf.NzRI_Zw_Fg);
        }

        /// <summary>
        /// 是否有非罡状态
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static bool HasFeiGangHediff(Pawn pawn)
        {
            return GetFeiGang(pawn) != null;
        }

        /// <summary>
        /// 在影响区域内获取所有Pawn
        /// </summary>
        /// <param name="map"></param>
        /// <param name="affectedCells"></param>
        /// <returns></returns>
        public static List<Pawn> GetPawnsInAffectedArea(Map map, List<IntVec3> affectedCells)
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

        /// <summary>
        /// 在影响区域内获取所有Pawn, 并且对其执行action
        /// </summary>
        /// <param name="map"></param>
        /// <param name="affectedCells"></param>
        /// <param name="action"></param>
        /// <returns>影响的Pawn数量</returns>
        public static int ApplyPawnInAffectedArea(Map map, List<IntVec3> affectedCells, System.Action<Pawn> action)
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

        /// <summary>
        /// 获取Pawn的所有物品, 包括携带物品, 穿戴装备, 装备武器
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static List<Thing> GetAllItems(this Pawn pawn)
        {
            List<Thing> allItems = new List<Thing>();

            // 获取Pawn的携带物品
            if (pawn.inventory != null)
            {
                allItems.AddRange(pawn.inventory.innerContainer.InnerListForReading);
            }

            // 获取Pawn的穿戴装备
            if (pawn.apparel != null)
            {
                allItems.AddRange(pawn.apparel.WornApparel);
            }

            // 获取Pawn的装备武器
            if (pawn.equipment != null)
            {
                allItems.AddRange(pawn.equipment.AllEquipmentListForReading);
            }
            
            return allItems;
        }

    }
}