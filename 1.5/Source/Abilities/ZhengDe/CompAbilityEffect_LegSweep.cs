using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Verse.Sound;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using HarmonyLib;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_LegSweep : CompAbilityEffect
    {

        private new CompProperties_LegSweep Props => (CompProperties_LegSweep)props;

        private Pawn Caster => parent.pawn;

        public void playEffect()
        {
            // 生成特效
            Mote_SpinningLine mote = (Mote_SpinningLine)ThingMaker.MakeThing(ThingDef.Named("NzRI_Mote_LegSweep"), null);
            if (mote != null)
            {
                mote.caster = Caster;
                mote.range = Props.radius;
                mote.speed = 3f;
                mote.lineSize = 2;
                GenSpawn.Spawn(mote, Caster.Position, Caster.Map);
            }
        }


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            playEffect();

            var affectedCells = AffectedCells(Caster);

            var damage = Props.damage;
            if (Props.damageMultiplierStat != null)
            {
                damage = Mathf.RoundToInt(damage * Caster.GetStatValue(Props.damageMultiplierStat));
            }

            // 人员 - 部位列表缓存
            var pawnBodyPartCache = new ConcurrentDictionary<Pawn, List<BodyPartRecord>>();
            var partDefs = Props.preferredAttackBodyParts;

            // 对范围内的Pawn的特定部位造成伤害(没有这个部位则随机)
            Parallel.ForEach(affectedCells, cell =>
            {
                var pawn = cell.GetFirstPawn(Caster.Map);
                if (pawn != null)
                {
                    var bodyPartRecords = new List<BodyPartRecord>();
                    foreach (var partDef in partDefs)
                    {
                        bodyPartRecords = pawn.health.hediffSet.GetNotMissingParts().Where(x => x.def == partDef).ToList();
                        if (bodyPartRecords.Count() > 0)
                        {
                            break;
                        }
                    }
                    // 获取目标身上所有的特定部位
                    if (bodyPartRecords.Count() == 0)
                    {
                        bodyPartRecords.Add(pawn.health.hediffSet.GetNotMissingParts().RandomElement());
                    }

                    pawnBodyPartCache.GetOrAdd(pawn, bodyPartRecords);
                }
            });

            // 在主线程中循环执行操作, 对每个Pawn的所有特定部位造成伤害
            foreach (var pawn in pawnBodyPartCache.Keys)
            {
                pawnBodyPartCache[pawn].ForEach(bodyPart =>
                {
                    var damageInfo = new DamageInfo(Props.damageDef, damage, Props.armorPenetration, -1, Caster, bodyPart);
                    pawn.TakeDamage(damageInfo);
                });
            }

            SoundDefOf.Execute_Cut.PlayOneShot(new TargetInfo(target.Cell, parent.pawn.Map, false));
            base.Apply(target, dest);
        }

        public override void DrawEffectPreview(LocalTargetInfo target)
        {
            GenDraw.DrawFieldEdges(AffectedCells(target));
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            // if (Pawn.Faction != null)
            // {
            //     foreach (IntVec3 item in AffectedCells(target))
            //     {
            //         List<Thing> thingList = item.GetThingList(Pawn.Map);
            //         for (int i = 0; i < thingList.Count; i++)
            //         {
            //             if (thingList[i].Faction == Pawn.Faction)
            //             {
            //                 return false;
            //             }
            //         }
            //     }
            // }

            return false;
        }

        private List<IntVec3> AffectedCells(LocalTargetInfo target)
        {
            // 获取以目标为中心的半径为radius的圆形区域
            return GenRadial.RadialCellsAround(target.Cell, Props.radius, false).ToList();
        }

    }
}