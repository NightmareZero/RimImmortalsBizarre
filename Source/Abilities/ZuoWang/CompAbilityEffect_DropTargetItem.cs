using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_DropTargetItem : CompAbilityEffect
    {
        public new CompProperties_DropTargetItem Props => (CompProperties_DropTargetItem)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            defaultProp();
            var targetPawn = target.Pawn;
            if (targetPawn == null || targetPawn == Caster)
            {
                return;
            }

#if DEBUG
            Log.Message(targetPawn.Name+" inventory:");
            targetPawn.inventory.innerContainer.InnerListForReading.ForEach(x =>
            {
                Log.Message(x.def.label.Translate());
                x.def.thingCategories.ForEach(y =>
                {
                    Log.Message(":" + y.defName);
                });
            });
#endif

            if (this.GetCastSuccess())
            {

                var droped = false;
                foreach (var category in Props.allowTypes)
                {
                    // 尝试获取一个符合条件的物品
                    var item = targetPawn.inventory.innerContainer.InnerListForReading.Where(x => x.def.thingCategories != null && x.def.thingCategories.Contains(category)).FirstOrDefault();
                    if (item != null)
                    {
                        GenPlace.TryPlaceThing(item, targetPawn.Position, targetPawn.Map, ThingPlaceMode.Near);
                        targetPawn.inventory.innerContainer.Remove(item);
                        droped = true;
                        break; // 成功掉落一个物品后终止
                    }
                }
                // TODO Message
                if (droped)
                {
                    Messages.Message("NzRI_Zw_DropTargetItem_Droped".Translate(targetPawn.Name.Named("Pawn")), MessageTypeDefOf.PositiveEvent);
                }
                else
                {
                    Messages.Message("NzRI_Zw_DropTargetItem_NoItem".Translate(targetPawn.Name.Named("Pawn")), MessageTypeDefOf.RejectInput);
                }
            }
            else
            {
                Messages.Message("NzRI_Zw_Is_Cheater".Translate(targetPawn.Name.Named("Pawn"), Caster.Name.Named("Caster")), MessageTypeDefOf.RejectInput);
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(XmlOf.NzRI_CheatMe, Caster);
            }
            base.Apply(target, dest);
        }

        private void defaultProp()
        {
            if (Props.allowTypes == null || Props.allowTypes.Count == 0)
            {
                Props.allowTypes = new List<ThingCategoryDef>() {
                    ThingOf.ApparelUtility,
                    ThingOf.Weapons,
                    ThingOf.Apparel,
                    ThingOf.Root,
                };
            }
        }
    }
}