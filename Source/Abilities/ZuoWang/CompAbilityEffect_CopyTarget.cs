using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;
using System;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_CopyTarget : CompAbilityEffect
    {
        // CompObelisk_Duplicator
        // AnomalyUtility.TryDuplicatePawn_NewTemp
        public new CompProperties_CopyTarget Props => (CompProperties_CopyTarget)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            var targetPawn = target.Pawn;
            var targetItem = target.Thing;
#if DEBUG
            Log.Message("CompAbilityEffect_CopyTarget. Apply. targetPawn: " + targetPawn + " targetItem: " + targetItem);
#endif
            if (targetPawn != null)
            {
                if (CopyPawn(targetPawn))
                {
                    Log.Message("Nz_CopyPawn_Copied".Translate(Caster.LabelShort));
                }
                return;
            }
            if (targetItem != null)
            {
                if (CopyItem(targetItem))
                {
                    Log.Message("Nz_CopyItem_Copied".Translate(Caster.LabelShort));
                }
                return;
            }
        }

        public bool CopyPawn(Pawn targetPawn)
        {
            Pawn newPawn;
            if (Maker.TryDuplicatePawn(targetPawn, targetPawn.Position, targetPawn.Map, out newPawn, targetPawn.Faction))
            {
                // 被复制的角色的状态
                if (Props.TargetHediff != null)
                {
                    var hediff = HediffMaker.MakeHediff(Props.TargetHediff, targetPawn);
                    hediff.Severity = Props.TargetHediff.initialSeverity;
                    targetPawn.health.AddHediff(hediff);
                }
                // 新角色的状态
                if (Props.NewPawnHediff != null)
                {
                    var hediff = HediffMaker.MakeHediff(Props.NewPawnHediff, newPawn);
                    hediff.Severity = Props.NewPawnHediff.initialSeverity;
                    newPawn.health.AddHediff(hediff);
                }
                // 被复制的角色的想法
                if (Props.TargetMind != null)
                {
                    Caster.needs.mood.thoughts.memories.TryGainMemory(Props.TargetMind);
                }
                // 新角色的想法
                if (Props.NewPawnMind != null)
                {
                    newPawn.needs.mood.thoughts.memories.TryGainMemory(Props.NewPawnMind);
                }

                if (Props.copyJunk)
                {
#if DEBUG
                    Log.Message("Nz_CopyPawn_CopiedJunk".Translate(Caster.LabelShort));
#endif
                    CopyJunkPawnPatcher.PatchPawn(newPawn);
                }

                FleckMaker.Static(newPawn.Position, parent.pawn.Map, FleckDefOf.PsycastSkipFlashEntry, 0.72f);
                SoundDefOf.Psycast_Skip_Pulse.PlayOneShot(new TargetInfo(newPawn.Position, Caster.Map));

                return true;
            }
            return false;
        }

        public bool CopyItem(Thing targetItem)
        {
            // 创建一个新的物体实例
            Thing duplicateItem = ThingMaker.MakeThing(targetItem.def, targetItem.Stuff);
            // 复制原始物体的属性
            duplicateItem.stackCount = targetItem.stackCount;
            duplicateItem.HitPoints = targetItem.HitPoints;


            // 如果是残次品
            if (Props.copyJunk)
            {
                if (targetItem.stackCount > 1)
                {
                    duplicateItem.stackCount = targetItem.stackCount / 2;
                }
                else
                {
                    duplicateItem.HitPoints = Mathf.RoundToInt(duplicateItem.MaxHitPoints * 0.5f);
                }
            }

            // 尝试将复制的物体放置在目标位置附近
            GenPlace.TryPlaceThing(duplicateItem, targetItem.Position, targetItem.Map, ThingPlaceMode.Near);
            FleckMaker.Static(duplicateItem.Position, parent.pawn.Map, FleckDefOf.PsycastSkipFlashEntry, 0.72f);
            SoundDefOf.Psycast_Skip_Pulse.PlayOneShot(new TargetInfo(duplicateItem.Position, Caster.Map));

            return true;
        }

        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            var targetPawn = target.Pawn;

            if (targetPawn != null)
            {
                if (targetPawn.kindDef.isBoss && !Props.allowBoss)
                {
                    return false;
                }

                if (targetPawn.RaceProps.Humanlike && Props.allowHuman)
                {
                    return true;
                }

                if (targetPawn.RaceProps.Animal && Props.allowAnimal)
                {
                    return true;
                }

                if (targetPawn.RaceProps.IsMechanoid && Props.allowMech)
                {
                    return true;
                }
            }

            var targetItem = target.Thing;
            if (targetItem != null && Props.allowItem)
            {
                return true;
            }


            return false;
        }
    }
}