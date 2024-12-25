using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using RimWorld;
using Unity.Mathematics;
using UnityEngine;
using Verse;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    // 暂存hediff直到本hediff被移除
    // !! 请注意会触发 PostAdd和PostRemoved, 提前检查hediff有没有特殊的代码
    public class Comp_StashHediff : HediffCompProperties
    {
        // 处理的hediff列表
        // !! 请注意会触发 PostAdd和PostRemoved, 提前检查hediff有没有特殊的代码
        public List<HediffDef> hediffs = new List<HediffDef>();



        // 当结束时是否加回来
        public bool addBack = false;

        public Comp_StashHediff()
        {
            compClass = typeof(HediffComp_StashHediff);
        }
    }

    public class HediffComp_StashHediff : HediffComp
    {
        public Comp_StashHediff Props => (Comp_StashHediff)props;

        public Dictionary<string, HediffDef> hediffCache = new Dictionary<string, HediffDef>();

        public List<Hediff> stashed = new List<Hediff>();

        public Pawn pawn => parent.pawn;

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);

            try
            {
                // 处理cache
                if (hediffCache.Count == 0 && Props.addBack)
                {
                    foreach (var hediffDef in Props.hediffs)
                    {
                        hediffCache[hediffDef.defName] = hediffDef;
                    }
                }

                // 遍历，移除并添加到 stashed
                if (pawn?.health?.hediffSet?.hediffs != null)
                {

                    foreach (var hediff in pawn.health.hediffSet.hediffs)
                    {
                        if (hediffCache.ContainsKey(hediff.def.defName))
                        {
                            stashed.Add(hediff);
                        }
                    }

                    foreach (var hediff in stashed)
                    {
                        pawn.health.RemoveHediff(hediff);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            if (pawn.Dead) return;

            try
            {
                if (Props.addBack)
                {
                    foreach (var hediff in stashed)
                    {
                        pawn.health.AddHediff(hediff);
                    }
                    stashed.Clear();
                }
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();

            Scribe_Collections.Look(ref stashed, "stashed", LookMode.Reference);
        }

    }
}