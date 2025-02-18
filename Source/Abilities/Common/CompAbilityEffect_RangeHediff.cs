using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;
using Verse.Sound;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_RangeHediff : CompAbilityEffect
    {
        private new CompProperties_AbilityRangeHediff Props => (CompProperties_AbilityRangeHediff)props;

        private Pawn Caster => parent.pawn;

        private Verb Verb => parent.verb;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            try
            {
                var affectedPawns = GetAffectedPawns(target.Cell, Caster.Map, Props.range);
                foreach (var pawn in affectedPawns)
                {
                    if (pawn != null)
                    {
                        if (Props.del)
                        {
                            var hediff = pawn.health.hediffSet.GetFirstHediffOfDef(Props.hediffDef);
                            if (hediff != null)
                            {
                                pawn.health.RemoveHediff(hediff);
                            }
                        }
                        else
                        {
                            var hediff = HediffMaker.MakeHediff(Props.hediffDef, pawn);
                            pawn.health.AddHediff(hediff);
                        }
                        if (Props.effectMote != null)
                        {
                            MoteMaker.MakeAttachedOverlay(pawn, Props.effectMote, Vector3.zero, 1f, -1f);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Error in CompAbilityEffect_RangeHediff.Apply: " + e);
            }
        }

        public override void DrawEffectPreview(LocalTargetInfo target)
        {
            GenDraw.DrawFieldEdges(getAffectedCells(target.Cell,Caster.Map,Props.range), Color.red);
        }

        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            return target.Cell.IsValid && target.Cell.InBounds(Caster.Map);
        }

        private List<IntVec3> getAffectedCells(IntVec3 center,Map map, float range)
        {
            var cells = GenRadial.RadialCellsAround(center, range, true).ToList();
            cells.RemoveAll(c => !c.InBounds(map));
            return cells;
        }

        private List<Pawn> GetAffectedPawns(IntVec3 center, Map map, float range)
        {
            var cells = getAffectedCells(center, map, range);
            var pawns = new List<Pawn>();
            foreach (var cell in cells)
            {
                var pawn = cell.GetFirstPawn(map);
                if (pawn != null && IsValidPawn(pawn))
                {
                    pawns.Add(pawn);
                }
            }
            return pawns;
        }


        private bool IsValidPawn(Pawn pawn)
        {
            // 根据Verb的TargetParameters来判断是否是合法的目标
            return Verb.targetParams.CanTarget(pawn);
        }
    }
}