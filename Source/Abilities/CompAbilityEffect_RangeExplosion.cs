using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_RangeExplosion : CompAbilityEffect
    {
        private readonly List<IntVec3> tmpCells = new List<IntVec3>();

        private new CompProperties_AbilityRangeExplosion Props => (CompProperties_AbilityRangeExplosion)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            GenExplosion.DoExplosion(target.Cell, parent.pawn.MapHeld, 0f, Props.damageTypes[0], Caster,
             postExplosionSpawnThingDef: Props.filthDef, damAmount: Props.damage, armorPenetration: -1f, explosionSound: null, weapon: null,
              projectile: null, intendedTarget: null, postExplosionSpawnChance: 1f, postExplosionSpawnThingCount: 1, postExplosionGasType: null,
               applyDamageToExplosionCellsNeighbors: false, preExplosionSpawnThingDef: null, preExplosionSpawnChance: 0f, preExplosionSpawnThingCount: 1,
                chanceToStartFire: 0f, damageFalloff: false, direction: null, ignoredThings: null, affectedAngle: null, doVisualEffects: false,
                 propagationSpeed: 0.6f, excludeRadius: 0f, doSoundEffects: false, postExplosionSpawnThingDefWater: null, screenShakeFactor: 1f,
                  flammabilityChanceCurve: parent.verb.verbProps.flammabilityAttachFireChanceCurve, overrideCells: TargetCells(target));
            base.Apply(target, dest);
        }

        public override IEnumerable<PreCastAction> GetPreCastActions()
        {
            if (Props.effecterDef != null)
            {
                yield return new PreCastAction
                {
                    action = delegate (LocalTargetInfo a, LocalTargetInfo b)
                    {
                        parent.AddEffecterToMaintain(Props.effecterDef.Spawn(parent.pawn.Position, a.Cell, parent.pawn.Map), Caster.Position, a.Cell, 17, Caster.MapHeld);
                    },
                    ticksAwayFromCast = 17
                };
            }
        }

        public override void DrawEffectPreview(LocalTargetInfo target)
        {
            GenDraw.DrawFieldEdges(TargetCells(target));
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return false;
        }

        private List<IntVec3> TargetCells(LocalTargetInfo target)
        {
            tmpCells.Clear();
            Vector3 vector = Caster.Position.ToVector3Shifted().Yto0();
            IntVec3 intVec = target.Cell.ClampInsideMap(Caster.Map);
            if (Caster.Position == intVec)
            {
                return tmpCells;
            }

            float targetRange = Props.targetRange; // 获取 targetRange 的值

            foreach (IntVec3 intVec2 in GenRadial.RadialCellsAround(target.Cell, targetRange, true))
            {
                if (intVec2.InBounds(Caster.Map) && CanUseCell(intVec2))
                {
                    tmpCells.Add(intVec2);
                }
            }

            return tmpCells;

        }

        private bool CanUseCell(IntVec3 c)
        {

            if (!c.InBounds(Caster.Map))
            {
                return false;
            }

            if (c == Caster.Position)
            {
                return false;
            }

            ShootLine resultingLine;
            return parent.verb.TryFindShootLineFromTo(parent.pawn.Position, c, out resultingLine);
        }
    }
}