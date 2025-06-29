using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_AreaAddHediff : CompAbilityEffect
    {
        private new CompProperties_AreaAddHediff Props => (CompProperties_AreaAddHediff)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (Props.hediffDef == null)
            {
                Log.Error("CompAbilityEffect_AreaAddHediff: hediffDef is null");
                return;
            }

            if (Props.target == CompProperties_AreaAddHediff.TargetSelf)
            {
                if (parent.pawn != null && (Props.hediffDef?.Count ?? 0) > 0)
                {
                    Props.hediffDef.ForEach((hediff) =>
                    {
                        parent.pawn.health.AddHediff(hediff);
                    });
                }
            }
            else if (Props.target == CompProperties_AreaAddHediff.TargetMap)
            {
                if ((Props.hediffDef?.Count ?? 0) > 0)
                {
                    EachFilteredPawnDo(Find.CurrentMap.mapPawns.AllPawns, (pawn) =>
                    {
                        Props.hediffDef.ForEach((hediff) =>
                        {
                            pawn.health.AddHediff(hediff);
                        });
                    });
                }
            }
            else if (Props.target == CompProperties_AreaAddHediff.TargetSelected)
            {
                if ((Props.hediffDef?.Count ?? 0) > 0)
                {
                    EachFilteredPawnDo(SelectPawn(), (pawn) =>
                    {
                        Props.hediffDef.ForEach((hediff) =>
                        {
                            pawn.health.AddHediff(hediff);
                        });
                    });
                }
            }
        }


        /// <summary>
        /// 对筛选后的Pawn列表执行操作
        /// </summary>
        /// <param name="pawns"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public List<Pawn> EachFilteredPawnDo(List<Pawn> pawns, Action<Pawn> action)
        {
            List<Pawn> result = new List<Pawn>();
            #if DEBUG
            Dictionary<string, int> count = new Dictionary<string, int>();
            #endif
            foreach (Pawn pawn in pawns)
            {
                if (!Props.noFilter)
                {
                    if (pawn == null)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo pawn is null");
                        count["null"] = count.ContainsKey("null") ? count["null"] + 1 : 1;
                        #endif
                        continue;
                    }

                    if (Props.noSelf && pawn == parent.pawn)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo noSelf: {pawn.Name}");
                        count["noSelf"] = count.ContainsKey("noSelf") ? count["noSelf"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toEnemies && parent.pawn.Faction != null && pawn.Faction != null && parent.pawn.Faction.HostileTo(pawn.Faction))
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo toEnemies: {pawn.Name}");
                        count["toEnemies"] = count.ContainsKey("toEnemies") ? count["toEnemies"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toAllies && parent.pawn.Faction != null && pawn.Faction != null && parent.pawn.Faction == pawn.Faction)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo toAllies: {pawn.Name}");
                        count["toAllies"] = count.ContainsKey("toAllies") ? count["toAllies"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toNeutral && parent.pawn.Faction != null && pawn.Faction != null && parent.pawn.Faction != pawn.Faction && !parent.pawn.Faction.HostileTo(pawn.Faction))
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo toNeutral: {pawn.Name}");
                        count["toNeutral"] = count.ContainsKey("toNeutral") ? count["toNeutral"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toColonists && pawn.Faction != null && pawn.Faction.IsPlayer)
                    {
                        #if DEBUG
                        count["toColonists"] = count.ContainsKey("toColonists") ? count["toColonists"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toAnimals && pawn.RaceProps.Animal)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo toAnimals: {pawn.Name}");
                        count["toAnimals"] = count.ContainsKey("toAnimals") ? count["toAnimals"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toHuman && pawn.RaceProps.Humanlike)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo toHuman: {pawn.Name}");
                        count["toHuman"] = count.ContainsKey("toHuman") ? count["toHuman"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (!Props.toMechanoids && pawn.RaceProps.IsMechanoid)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo toMechanoids: {pawn.Name}");
                        count["toMechanoids"] = count.ContainsKey("toMechanoids") ? count["toMechanoids"] + 1 : 1;
                        #endif
                        continue;
                    }
                    if (Props.notBoss && pawn.kindDef.isBoss)
                    {
                        #if DEBUG
                        Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo notBoss: {pawn.Name}");
                        count["notBoss"] = count.ContainsKey("notBoss") ? count["notBoss"] + 1 : 1;
                        #endif
                        continue;
                    }
                    #if DEBUG
                    Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo: {pawn.Name}");
                    count["default"] = count.ContainsKey("default") ? count["default"] + 1 : 1;
                    #endif
                }
                if (action != null)
                {
                    #if DEBUG
                    Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo action: {pawn.Name}");
                    count["action"] = count.ContainsKey("action") ? count["action"] + 1 : 1;
                    #endif
                    action(pawn);
                }
                result.Add(pawn);
            }
            #if DEBUG
            foreach (var item in count)
            {
                Log.Message($"CompAbilityEffect_AreaAddHediff: EachFilteredPawnDo count: {item.Key} - {item.Value}");
            }
            #endif
            return result;
        }

        /// <summary>
        /// 获取筛选后的Pawn列表
        /// </summary>
        /// <param name="pawns"></param>
        /// <returns></returns>
        public List<Pawn> GetFilterPawns(List<Pawn> pawns)
        {
            return EachFilteredPawnDo(pawns, null);
        }

        public virtual List<Pawn> SelectPawn()
        {
            return GetFilterPawns(Find.CurrentMap.mapPawns.AllPawns);
        }
    }
}