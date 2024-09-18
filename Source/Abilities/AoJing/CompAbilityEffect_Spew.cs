using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{
    // CompAbilityEffect_FireSpew
    public class CompAbilityEffect_Spew : CompAbilityEffect, DamagePatcher
    {
        private readonly List<IntVec3> tmpCells = new List<IntVec3>();

        private new CompProperties_AbilitySpew Props => (CompProperties_AbilitySpew)props;

        private Pawn Caster => parent.pawn;

        private float damageMultiplier = 1f;

        private float armorPenetrationMultiplier = 1f;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            Patcher.DoDamagePatch(this,this);

            var damage = Props.damAmount * damageMultiplier;
            var armorPenetration = 1f * armorPenetrationMultiplier;
            List<IntVec3> affected = AffectedCells(target);
            GenExplosion.DoExplosion(target.Cell, parent.pawn.MapHeld, 0f, Props.damageType, Caster,
             postExplosionSpawnThingDef: Props.filthDef, damAmount: (int)damage, armorPenetration: armorPenetration, explosionSound: null, weapon: null,
              projectile: null, intendedTarget: null, postExplosionSpawnChance: 1f, postExplosionSpawnThingCount: 1, postExplosionGasType: null,
               applyDamageToExplosionCellsNeighbors: false, preExplosionSpawnThingDef: null, preExplosionSpawnChance: 0f, preExplosionSpawnThingCount: 1,
                chanceToStartFire: 0f, damageFalloff: false, direction: null, ignoredThings: null, affectedAngle: null, doVisualEffects: false,
                 propagationSpeed: 0.6f, excludeRadius: 0f, doSoundEffects: false, postExplosionSpawnThingDefWater: null, screenShakeFactor: 1f,
                  flammabilityChanceCurve: parent.verb.verbProps.flammabilityAttachFireChanceCurve, overrideCells: affected);

            // 在影响区域内获取所有Pawn, 并且对非自己的Pawn附加一个30秒的剧痛Hediff
            var pawnNum = Utils.ApplyPawnInAffectedArea(Caster.Map, affected, delegate (Pawn p)
            {
                if (p != Caster && p.Faction != Caster.Faction)
                {
                    // 附加一个30秒的剧痛Hediff
                    Hediff hediff = HediffMaker.MakeHediff(XmlOf.NzRI_AoJing_Agony, p);
                    p.health.AddHediff(hediff);
                }
            });

            #if DEBUG
            Log.Message("NzRI_Spew_Apply PawnNum: " + pawnNum);
            #endif

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
            GenDraw.DrawFieldEdges(AffectedCells(target));
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            if (Caster.Faction != null)
            {
                foreach (IntVec3 item in AffectedCells(target))
                {
                    List<Thing> thingList = item.GetThingList(Caster.Map);
                    for (int i = 0; i < thingList.Count; i++)
                    {
                        if (thingList[i].Faction == Caster.Faction)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        private List<IntVec3> AffectedCells(LocalTargetInfo target)
        {
            tmpCells.Clear();
            Vector3 vector = Caster.Position.ToVector3Shifted().Yto0();
            IntVec3 intVec = target.Cell.ClampInsideMap(Caster.Map);
            if (Caster.Position == intVec)
            {
                return tmpCells;
            }

            float lengthHorizontal = (intVec - Caster.Position).LengthHorizontal;
            float num = (float)(intVec.x - Caster.Position.x) / lengthHorizontal;
            float num2 = (float)(intVec.z - Caster.Position.z) / lengthHorizontal;
            intVec.x = Mathf.RoundToInt((float)Caster.Position.x + num * Props.range);
            intVec.z = Mathf.RoundToInt((float)Caster.Position.z + num2 * Props.range);
            float target2 = Vector3.SignedAngle(intVec.ToVector3Shifted().Yto0() - vector, Vector3.right, Vector3.up);
            float num3 = Props.lineWidthEnd / 2f;
            float num4 = Mathf.Sqrt(Mathf.Pow((intVec - Caster.Position).LengthHorizontal, 2f) + Mathf.Pow(num3, 2f));
            float num5 = 57.29578f * Mathf.Asin(num3 / num4);
            int num6 = GenRadial.NumCellsInRadius(Props.range);
            for (int i = 0; i < num6; i++)
            {
                IntVec3 intVec2 = Caster.Position + GenRadial.RadialPattern[i];
                if (CanUseCell(intVec2) && Mathf.Abs(Mathf.DeltaAngle(Vector3.SignedAngle(intVec2.ToVector3Shifted().Yto0() - vector, Vector3.right, Vector3.up), target2)) <= num5)
                {
                    tmpCells.Add(intVec2);
                }
            }

            List<IntVec3> list = GenSight.BresenhamCellsBetween(Caster.Position, intVec);
            for (int j = 0; j < list.Count; j++)
            {
                IntVec3 intVec3 = list[j];
                if (!tmpCells.Contains(intVec3) && CanUseCell(intVec3))
                {
                    tmpCells.Add(intVec3);
                }
            }

            return tmpCells;
            bool CanUseCell(IntVec3 c)
            {
                if (!c.InBounds(Caster.Map))
                {
                    return false;
                }

                if (c == Caster.Position)
                {
                    return false;
                }

                if (!Props.canHitFilledCells && c.Filled(Caster.Map))
                {
                    return false;
                }

                if (!c.InHorDistOf(Caster.Position, Props.range))
                {
                    return false;
                }

                ShootLine resultingLine;
                return parent.verb.TryFindShootLineFromTo(parent.pawn.Position, c, out resultingLine);
            }
        }

        public int PatchType()
        {
            return Props.skillRoute;
        }

        public void SetDamageMultiplier(float multiplier)
        {
            damageMultiplier = multiplier;
        }

        public void SetArmorPenetrationMultiplier(float penetration)
        {
            armorPenetrationMultiplier = penetration;
        }
    }
}

/*
GenExplosion.DoExplosion

target.Cell: 爆炸发生的目标位置。
parent.pawn.MapHeld: 爆炸发生的地图。
0f: 爆炸半径（这里为0，表示没有实际爆炸范围）。
DamageDefOf.Flame: 爆炸造成的伤害类型（这里是火焰伤害）。
Pawn: 造成爆炸的对象。
postExplosionSpawnThingDef: Props.filthDef: 爆炸后生成的物品类型。
damAmount: Props.damAmount: 爆炸造成的伤害量。
armorPenetration: -1f: 爆炸的护甲穿透力（-1表示默认值）。
explosionSound: null: 爆炸声音（null表示默认声音）。
weapon: null: 造成爆炸的武器（null表示没有特定武器）。
projectile: null: 造成爆炸的弹药（null表示没有特定弹药）。
intendedTarget: null: 爆炸的目标（null表示没有特定目标）。
postExplosionSpawnChance: 1f: 爆炸后生成物品的概率（1表示100%）。
postExplosionSpawnThingCount: 1: 爆炸后生成物品的数量。
postExplosionGasType: null: 爆炸后生成的气体类型（null表示没有气体）。
applyDamageToExplosionCellsNeighbors: false: 是否对爆炸范围内的邻近单元格造成伤害。
preExplosionSpawnThingDef: null: 爆炸前生成的物品类型（null表示没有物品）。
preExplosionSpawnChance: 0f: 爆炸前生成物品的概率（0表示不会生成）。
preExplosionSpawnThingCount: 1: 爆炸前生成物品的数量。
chanceToStartFire: 1f: 爆炸后点燃火焰的概率（1表示100%）。
damageFalloff: false: 是否随距离衰减伤害。
direction: null: 爆炸方向（null表示无特定方向）。
ignoredThings: null: 被忽略的对象（null表示没有忽略对象）。
affectedAngle: null: 受影响的角度（null表示全方位）。
doVisualEffects: false: 是否显示视觉效果。
propagationSpeed: 0.6f: 爆炸传播速度。
excludeRadius: 0f: 排除的半径（0表示没有排除）。
doSoundEffects: false: 是否播放声音效果。
postExplosionSpawnThingDefWater: null: 爆炸后在水中生成的物品类型（null表示没有物品）。
screenShakeFactor: 1f: 屏幕震动因子。
flammabilityChanceCurve: parent.verb.verbProps.flammabilityAttachFireChanceCurve: 可燃性概率曲线。
overrideCells: AffectedCells(target): 覆盖的单元格。
*/