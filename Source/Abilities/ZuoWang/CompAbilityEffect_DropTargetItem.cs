using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_DropTargetItem : CompAbilityEffect_ZuoWangBase { 
        public new CompProperties_DropTargetItem Props => (CompProperties_DropTargetItem)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            var targetPawn = target.Pawn;
            if (targetPawn == null || targetPawn == Caster)
            {
                return;
            }

            if (GetCastSuccess())
            {
                foreach (var category in Props.allowTypes)
            {
                var item = targetPawn.inventory.innerContainer.FirstOrDefault(x => x.def.thingCategories != null && x.def.thingCategories.Contains(category));
                if (item != null)
                {
                    targetPawn.inventory.innerContainer.Remove(item);
                    GenPlace.TryPlaceThing(item, targetPawn.Position, targetPawn.Map, ThingPlaceMode.Near);
                    break; // 成功掉落一个物品后终止
                }
            }
            }
            base.Apply(target, dest);
        }
    }
}