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
                if (parent.pawn != null)
                {
                    parent.pawn.health.AddHediff(Props.hediffDef);
                }
            }
            else if (Props.target == CompProperties_AreaAddHediff.TargetMap)
            {
                EachFilteredPawnDo(Find.CurrentMap.mapPawns.AllPawns, (pawn) =>
                {
                    pawn.health.AddHediff(Props.hediffDef);
                });
            }
            else if (Props.target == CompProperties_AreaAddHediff.TargetSelected)
            {
                EachFilteredPawnDo(SelectPawn(), (pawn) =>
                {
                    pawn.health.AddHediff(Props.hediffDef);
                });
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
            foreach (Pawn pawn in pawns)
            {
                if (!Props.noFilter)
                {

                    if (Props.noSelf && pawn == parent.pawn)
                    {
                        continue;
                    }
                    if (Props.toEnemies && parent.pawn.Faction != null && pawn.Faction != null && parent.pawn.Faction.HostileTo(pawn.Faction))
                    {
                        continue;
                    }
                    if (Props.toAllies && parent.pawn.Faction != null && pawn.Faction != null && parent.pawn.Faction == pawn.Faction)
                    {
                        continue;
                    }
                    if (Props.toNeutral && parent.pawn.Faction != null && pawn.Faction != null && parent.pawn.Faction != pawn.Faction && !parent.pawn.Faction.HostileTo(pawn.Faction))
                    {
                        continue;
                    }
                    if (Props.toColonists && pawn.Faction != null && pawn.Faction.IsPlayer)
                    {
                        continue;
                    }
                    if (Props.toAnimals && pawn.RaceProps.Animal)
                    {
                        continue;
                    }
                    if (Props.toHuman && pawn.RaceProps.Humanlike)
                    {
                        continue;
                    }
                    if (Props.toMechanoids && pawn.RaceProps.IsMechanoid)
                    {
                        continue;
                    }
                    if (Props.notBoss && pawn.kindDef.isBoss)
                    {
                        continue;
                    }
                }
                if (action != null)
                {
                    action(pawn);
                }
                result.Add(pawn);
            }
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