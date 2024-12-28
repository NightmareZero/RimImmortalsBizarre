using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    public class Comp_ProductionQualityOffset : HediffCompProperties
    { 
        // 质量偏移
        public int offset = 0;

        public Comp_ProductionQualityOffset()
        {
            compClass = typeof(HediffComp_ProductionQualityOffset);
        }

    }

    public class HediffComp_ProductionQualityOffset : HediffComp
    {
        public Comp_ProductionQualityOffset Props => (Comp_ProductionQualityOffset)props;

        public int offset => Props.offset;

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();
            Caches.productionQualityOffsetCache.Remove(parent.pawn);
        }

        public override void CompPostMake()
        {
            base.CompPostMake();
            Caches.productionQualityOffsetCache[parent.pawn] = this;
        }
    }

    // 由于本身没有提供修改质量的方法，所以只能通过 Harmony 修改
    [HarmonyPatch(typeof(QualityUtility))]
    [HarmonyPatch(nameof(QualityUtility.GenerateQualityCreatedByPawn))]
    [HarmonyPatch(new Type[] { typeof(Pawn), typeof(SkillDef) })]
    public static class HarmonyPatchProductionQualityOffset
    {
        public static void Postfix(ref QualityCategory __result, Pawn pawn, SkillDef relevantSkill)
        {
            try
            {
                // 尝试从缓存中获取 HediffComp_ProductionQualityOffset
                if (Caches.productionQualityOffsetCache.TryGetValue(pawn, out var comp))
                {
                    if (comp == null) return;
                }
                else
                {
                    // 缓存中没有，遍历 HediffSet 获取 Comp_ProductionQualityOffset
                    var hediffWithComp = pawn.health.hediffSet.hediffs
                        .OfType<HediffWithComps>()
                        .FirstOrDefault(h => h.TryGetComp<HediffComp_ProductionQualityOffset>() != null);

                    comp = hediffWithComp?.TryGetComp<HediffComp_ProductionQualityOffset>();
                    // 缓存
                    Caches.productionQualityOffsetCache[pawn] = comp;
                    if (comp == null) return;
                }

                // 根据 offset 调整质量
                __result = (QualityCategory)Mathf.Clamp((int)__result + comp.offset, (int)QualityCategory.Awful, (int)QualityCategory.Legendary);
            }
            catch (Exception e)
            {
                Log.Error($"NzRimImmortalBizarre: HarmonyPatchProductionQualityOffset error: {e}");
            }
        }

    }
}