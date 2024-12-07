using Verse;
using RimWorld;
using System.Collections.Generic;
using WhoXiuXian;
using Core;
using System.Linq;
using System;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    {
        /// <summary>
        /// 尝试获取灵根
        /// (如果没有则返回null)
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Hediff_RI_EnergyRoot TryGetEnergyRoot(this Pawn pawn)
        {
            return pawn?.health?.hediffSet?.GetFirstHediffOfDef(RI_DefOf.Hediff_RI_EnergyRoot) as Hediff_RI_EnergyRoot;
        }

        /// <summary>
        /// 获取登阶Hediff
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Hediff GetAscendHediff(Pawn pawn)
        {
            return pawn.health?.hediffSet?.GetFirstHediffOfDef(XmlOf.NzRI_AoJing_Ascend);
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
        public static Zw_Fg GetFeiGangHediff(Pawn pawn)
        {
            return (Zw_Fg)pawn.health?.hediffSet?.GetFirstHediffOfDef(XmlOf.NzRI_Zw_Fg);
        }

        /// <summary>
        /// 获取非罡Hediff, 如果没有则创建一个
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Zw_Fg AssertGetFeiGangHediff(Pawn pawn)
        {
            var fg = GetFeiGangHediff(pawn);
            if (fg == null)
            {
                Log.Message("Pawn " + pawn.Name + " has no FeiGang hediff. Creating one.");
                // 生成一个非罡状态
                fg = HediffMaker.MakeHediff(XmlOf.NzRI_Zw_Fg, pawn) as Zw_Fg;
                pawn.health.AddHediff(fg);
            }
            return fg;
        }

        /// <summary>
        /// 是否有非罡状态
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static bool HasFeiGangHediff(Pawn pawn)
        {
            return GetFeiGangHediff(pawn) != null;
        }

        /// <summary>
        /// 获取果位Hediff, 如果没有则创建一个
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Zd_Fruition AssertGetFruitionHediff(Pawn pawn)
        {
            var fruition = GetFruitionHediff(pawn);
            if (fruition == null)
            {
                Log.Message("Pawn " + pawn.Name + " has no Fruition hediff. Creating one.");
                // 生成一个果位状态
                fruition = HediffMaker.MakeHediff(XmlOf.NzRI_Zd_Fruition, pawn) as Zd_Fruition;
                pawn.health.AddHediff(fruition);
            }
            return fruition;
        }

        /// <summary>
        /// 获取果位Hediff
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Zd_Fruition GetFruitionHediff(Pawn pawn)
        {
            return (Zd_Fruition)pawn.health?.hediffSet?.GetFirstHediffOfDef(XmlOf.NzRI_Zd_Fruition);
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
        /// 在影响区域内获取所有Pawn
        /// </summary>
        /// <param name="map">所在地图</param>
        /// <param name="center">中心格</param>
        /// <param name="radius">范围</param>
        /// <returns></returns>
        public static List<Pawn> GetPawnsInAffectedArea(Map map, IntVec3 center, float radius)
        {
            return GenRadial.RadialCellsAround(center, radius, true)
                .Where(cell => cell.InBounds(map)) // 检查 cell 是否在地图边界内
                .SelectMany(cell => cell.GetThingList(map).OfType<Pawn>())
                .ToList();
        }

        /// <summary>
        /// 在影响区域内获取所有Pawn, 并且对其执行action
        /// </summary>
        /// <param name="map">所在地图</param>
        /// <param name="center">中心格</param>
        /// <param name="radius">范围</param>
        /// <param name="action">对Pawn的操作</param>
        /// <returns></returns>
        public static int ApplyPawnInAffectedArea(Map map, IntVec3 center, float radius, System.Action<Pawn> action)
        {
            var count = 0;
            GenRadial.RadialCellsAround(center, radius, true)
                .Where(cell => cell.InBounds(map)) // 检查 cell 是否在地图边界内
                .SelectMany(cell => cell.GetThingList(map).OfType<Pawn>())
                .All(pawn =>
                {
                    try
                    {
                        action(pawn);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        ex.PrintExceptionWithStackTrace();
                    }
                    return true;
                });
                
            return count;
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
                        try
                        {
                            action(pawn);
                        }
                        catch (Exception ex)
                        {
                            ex.PrintExceptionWithStackTrace();
                        }
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


        /// <summary>
        /// 计算Pawn的心素部位数量
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static int GetXinSuPartCount(this Pawn pawn)
        {
            int count = 0;
            foreach (Hediff hediff in pawn?.health?.hediffSet?.hediffs)
            {
                if (hediff.def.tags == null)
                {
                    continue;
                }
                hediff.def.tags.ForEach(tag =>
                {
                    if (tag == TagDefOf.NzRI_XinSuBodyPart.defName)
                    {
                        count++;
                    }
                });
            }
            return count;
        }


        /// <summary>
        /// 检查Pawn是否有灵芽
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static bool HasRiEnergyRoot(this Pawn pawn)
        {
            return pawn.health.hediffSet.HasHediff(RI_DefOf.Hediff_RI_EnergyRoot);
        }

        /// <summary>
        /// 获取Pawn的境界Hediff
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static Hediff_RI_EnergyRoot GetRiEnergyRoot(this Pawn pawn)
        {
            return pawn.HasRiEnergyRoot() ? ((Hediff_RI_EnergyRoot)pawn.health.hediffSet.GetFirstHediffOfDef(RI_DefOf.Hediff_RI_EnergyRoot)) : null;
        }

        /// <summary>
        /// 获取Pawn的境界等级
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        public static int GetRiEnergyRootLevel(this Pawn pawn)
        {
            return pawn.GetRiEnergyRoot()?.energy?.currentRI?.def?.level ?? 0;
        }

        /// <summary>
        /// 尝试获取最糟糕的健康状况(包括不可治愈的成瘾)
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="worstHealth"></param>
        /// <param name="worstBodyPart"></param>
        /// <returns></returns>
        public static bool TryGetWorstHealthCondition(Pawn pawn, out Hediff worstHealth, out BodyPartRecord worstBodyPart)
        {
            var get = HealthUtility.TryGetWorstHealthCondition(pawn, out worstHealth, out worstBodyPart);
            if (get)
            {
                return true;
            }

            // 找到身上一处成瘾
            var add = FindAllAddiction(pawn, null);
            if (add != null)
            {
                worstHealth = add;
                worstBodyPart = add.Part;
                return true;
            }


            return false;
        }

        /// <summary>
        /// 寻找所有成瘾(包括不可治愈的)
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        private static Hediff_Addiction FindAllAddiction(Pawn pawn, params HediffDef[] exclude)
        {
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int i = 0; i < hediffs.Count; i++)
            {
                if (hediffs[i] is Hediff_Addiction addiction && addiction.Visible && (exclude == null || !exclude.Contains(hediffs[i].def)))
                {
                    return addiction;
                }
            }

            return null;
        }

        /// <summary>
        /// 尝试在地图边缘找到一个可达的入口
        /// </summary>
        /// <param name="map">地图</param>
        /// <param name="cell">入口</param>
        /// <returns></returns>
        public static bool TryFindEntryCell(Map map, out IntVec3 cell)
        {
            return CellFinder.TryFindRandomEdgeCellWith((IntVec3 c) => map.reachability.CanReachColony(c), map, CellFinder.EdgeRoadChance_Ignore, out cell);
        }

        /// <summary>
        /// 尝试找到一个人类派系
        /// </summary>
        /// <param name="formerFaction"></param>
        /// <returns></returns>
        public static bool TryFindFormerFaction(out Faction formerFaction)
        {
            return Find.FactionManager.TryGetRandomNonColonyHumanlikeFaction(out formerFaction, tryMedievalOrBetter: false, allowDefeated: true);
        }
    }
}