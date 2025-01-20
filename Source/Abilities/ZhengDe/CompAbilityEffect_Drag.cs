using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Drag : CompAbilityEffect { 

        public new CompProperties_Drag Props => (CompProperties_Drag)props;

        public Pawn caster => parent.pawn;
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Pawn == null) { 
                return;
            }
            // 将目标拉到面前

            // 获取施法者的位置最近的空位
            IntVec3 closestEmptyCell = CellFinder.RandomClosewalkCellNear(caster.Position, caster.Map, 1);

            // 如果没有空位，就不执行
            if (closestEmptyCell == IntVec3.Invalid) {
                return;
            }

            // 生成拖拽Mote
            Mote_Rope mote = (Mote_Rope)ThingMaker.MakeThing(ThingDef.Named("Mote_Rope"), null);
            mote.caster = caster;
            mote.target = target.Pawn;
            mote.Scale = 0.1f;
            mote.rotationRate = 0f;
            GenSpawn.Spawn(mote, target.Pawn.Position, caster.Map);
            

            // 将目标传送到这个位置
            target.Pawn.Position = closestEmptyCell;

            base.Apply(target, dest);
        }
    }
}