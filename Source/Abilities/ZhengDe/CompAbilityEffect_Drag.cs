using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Drag : CompAbilityEffect
    {

        public new CompProperties_Drag Props => (CompProperties_Drag)props;

        public Pawn caster => parent.pawn;
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Pawn == null)
            {
                return;
            }
            // 将目标拉到面前

            // 获取施法者的位置最近的空位
            IntVec3 closestEmptyCell = CellFinder.RandomClosewalkCellNear(caster.Position, caster.Map, 1);

            // 如果没有空位，就不执行
            if (closestEmptyCell == IntVec3.Invalid)
            {
                return;
            }

            // 生成拖拽Mote
            Mote_Rope mote = (Mote_Rope)ThingMaker.MakeThing(ThingDef.Named("NzRI_Mote_Rope"), null);
            if (mote != null)
            {
                mote.caster = caster;
                mote.target = target.Pawn;
                mote.Scale = 0.1f;
                mote.rotationRate = 0f;
                GenSpawn.Spawn(mote, target.Pawn.Position, caster.Map);
            }


            // 目标跳跃到这个位置
            PawnFlyer flyer = PawnFlyer.MakeFlyer(ThingDefOf.PawnFlyer, target.Pawn, closestEmptyCell, null, null);
            GenSpawn.Spawn(flyer, target.Pawn.Position, caster.Map);
            target.Pawn.stances.stunner.StunFor(120, caster, false);

            base.Apply(target, dest);
        }
    }
}