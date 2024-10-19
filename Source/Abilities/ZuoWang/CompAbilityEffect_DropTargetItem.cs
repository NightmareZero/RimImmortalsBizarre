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

            var targetPawnItems = targetPawn.GetAllItems();
#if DEBUG
            Log.Message(targetPawn.Name + " inventory:");
            targetPawnItems.ForEach(x =>
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
                    var item = targetPawnItems.Where(x => x.def.IsItemParentCategory(category)).FirstOrDefault();
                    if (item != null)
                    {
                        GenPlace.TryPlaceThing(item, targetPawn.Position, targetPawn.Map, ThingPlaceMode.Near);
                        targetPawn.inventory.innerContainer.Remove(item);
                        droped = true;
                        break; // 成功掉落一个物品后终止
                    }
                }

                if (droped)
                {
                    Messages.Message("NzRI_Zw_DropTargetItem_Droped".Translate(targetPawn.Name.Named("Pawn")), MessageTypeDefOf.PositiveEvent);
                    if (targetPawn != null && Caster != targetPawn)
                    {
                        Caster.interactions?.TryInteractWith(targetPawn, Thought1Def.WordOfJoy);
                    }
                }
                else
                {
                    Messages.Message("NzRI_Zw_DropTargetItem_NoItem".Translate(targetPawn.Name.Named("Pawn")), MessageTypeDefOf.RejectInput);
                }
            }
            else
            {
                Messages.Message("NzRI_Zw_Is_Cheater".Translate(targetPawn.Name.Named("Pawn"), Caster.Name.Named("Caster")), MessageTypeDefOf.RejectInput);
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(Thought1Def.NzRI_CheatMe, Caster);
                if (targetPawn != null && Caster != targetPawn)
                {
                    Caster.interactions?.TryInteractWith(targetPawn, Thought1Def.Slight);
                }
            }
            base.Apply(target, dest);
        }

        private void defaultProp()
        {
            if (Props.allowTypes == null)
            {
                Props.allowTypes = new List<ThingCategoryDef>() {
                    ThingOf.ApparelUtility,
                    ThingOf.Apparel,
                    ThingOf.Weapons,
                    ThingOf.Root,
                };

                // 异常检测
                Props.allowTypes.ForEach(x =>
                {
                    if (x == null)
                    {
                        Log.Error("CompAbilityEffect_DropTargetItem: allowTypes is null");
                    }
                });
            }
        }






    }
}