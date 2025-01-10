using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_RangeExplosion : CompAbilityEffect
    {


        private new CompProperties_AbilityRangeExplosion Props => (CompProperties_AbilityRangeExplosion)props;

        private Pawn Caster => parent.pawn;


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            // 读取Stat
            var damage = Props.damage;
            if (Props.damageFactorStat != null)
            {
                damage *= (int)Caster.GetStatValue(Props.damageFactorStat);
            }
            var armorPenetration = Props.armorPenetration;
            if (Props.armorPenetrationFactorStat != null)
            {
                armorPenetration *= Caster.GetStatValue(Props.armorPenetrationFactorStat);
            }

            if (Props.damageTypes.NullOrEmpty())
            {
                Props.damageTypes = new List<DamageDef> { DamageDefOf.Bomb };
                return;
            }
            
            for (int i = 0; i < Props.damageTimes; i++)
            {
                var targetCell = TargetAffectedCells(target).RandomElement();
                var explosionCells = GetRangeCells(targetCell, Props.explosionRange);

                GenExplosion.DoExplosion(target.Cell, parent.pawn.MapHeld, Props.explosionRange, Props.damageTypes.RandomElement(), Caster,
                    postExplosionSpawnThingDef: Props.filthDef, damAmount: damage, armorPenetration: armorPenetration, explosionSound: Props.explosionSound, weapon: null,
                    projectile: null, intendedTarget: null, postExplosionSpawnChance: 1f, postExplosionSpawnThingCount: 1, postExplosionGasType: null,
                    applyDamageToExplosionCellsNeighbors: false, preExplosionSpawnThingDef: null, preExplosionSpawnChance: 0f, preExplosionSpawnThingCount: 1,
                    chanceToStartFire: 0f, damageFalloff: false, direction: null, ignoredThings: null, affectedAngle: null, doVisualEffects: true,
                    propagationSpeed: 0.6f, excludeRadius: 0f, doSoundEffects: true, postExplosionSpawnThingDefWater: null, screenShakeFactor: 1f,
                    flammabilityChanceCurve: parent.verb.verbProps.flammabilityAttachFireChanceCurve, overrideCells: explosionCells);
            }

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
            GenDraw.DrawFieldEdges(TargetAffectedCells(target), Color.red);
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return false;
        }

        private List<IntVec3> TargetAffectedCells(LocalTargetInfo target)
        {
            IntVec3 intVec = target.Cell.ClampInsideMap(Caster.Map);
            if (Caster.Position == intVec)
            {
                return new List<IntVec3>();
            }

            float targetRange = Props.deviationRange; // 获取 targetRange 的值
            IntVec3 targetCell = target.Cell; // 获取目标位置

            return GetRangeCells(targetCell, targetRange);
        }

        private List<IntVec3> GetRangeCells(IntVec3 targetCell, float targetRange)
        {
            List<IntVec3> cells = new List<IntVec3>();
            foreach (IntVec3 intVec2 in GenRadial.RadialCellsAround(targetCell, targetRange, true))
            {
                if (CanUseCell(intVec2))
                {
                    cells.Add(intVec2);
                }
            }

            return cells;
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

            return true;
        }

    }
}