using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Spray : CompAbilityEffect
    {
        // CompAbilityEffect_SprayLiquid
        private List<Pair<IntVec3, float>> tmpCellDots = new List<Pair<IntVec3, float>>();

        private List<IntVec3> tmpCells = new List<IntVec3>();

        private new CompProperties_AbilitySpray Props => (CompProperties_AbilitySpray)props;

        private Pawn Pawn => parent.pawn;


        // TODO 读取参数修正伤害
        private float damageMultiplier = 1f;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            foreach (IntVec3 item in AffectedCells(target))
            {
                // TODO 想办法修正伤害
                ((Projectile)GenSpawn.Spawn(Props.projectileDef, Pawn.Position, Pawn.Map)).Launch(Pawn, Pawn.DrawPos, item, item, ProjectileHitFlags.IntendedTarget);
            }

            if (Props.sprayEffecter != null)
            {
                Props.sprayEffecter.Spawn(parent.pawn.Position, target.Cell, parent.pawn.Map).Cleanup();
            }

            base.Apply(target, dest);
        }

        public override void DrawEffectPreview(LocalTargetInfo target)
        {
            GenDraw.DrawFieldEdges(AffectedCells(target));
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            if (Pawn.Faction != null)
            {
                foreach (IntVec3 item in AffectedCells(target))
                {
                    List<Thing> thingList = item.GetThingList(Pawn.Map);
                    for (int i = 0; i < thingList.Count; i++)
                    {
                        if (thingList[i].Faction == Pawn.Faction)
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
            tmpCellDots.Clear();
            tmpCells.Clear();
            HashSet<IntVec3> visitedCells = new HashSet<IntVec3>();
            Queue<IntVec3> cellsToProcess = new Queue<IntVec3>();
        
            // 初始化队列和集合
            cellsToProcess.Enqueue(target.Cell);
            visitedCells.Add(target.Cell);
        
            while (cellsToProcess.Count > 0 && tmpCellDots.Count < Props.numCellsToHit)
            {
                IntVec3 currentCell = cellsToProcess.Dequeue();
                tmpCellDots.Add(new Pair<IntVec3, float>(currentCell, 0f));
        
                // 获取当前格子的邻居格子
                foreach (IntVec3 neighbor in GenAdjFast.AdjacentCells8Way(currentCell))
                {
                    if (!visitedCells.Contains(neighbor))
                    {
                        visitedCells.Add(neighbor);
                        cellsToProcess.Enqueue(neighbor);
                    }
                }
            }
        
            Map map = Pawn.Map;
            int num = Mathf.Min(tmpCellDots.Count, Props.numCellsToHit);
            Log.Message("numCellsToHit: " + Props.numCellsToHit + " num: " + num);
        
            for (int j = 0; j < num; j++)
            {
                IntVec3 first2 = tmpCellDots[j].First;
                if (!first2.InBounds(map))
                {
                    continue;
                }
        
                if (first2.Filled(map))
                {
                    Building_Door door = first2.GetDoor(map);
                    if (door == null || !door.Open)
                    {
                        continue;
                    }
                }
        
                if (parent.verb.TryFindShootLineFromTo(Pawn.Position, first2, out var _, ignoreRange: true))
                {
                    tmpCells.Add(first2);
                }
            }
        
            return tmpCells;
        }

    }
}