using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_FictionReality : CompAbilityEffect
    {
        public new FictionReality Props => (FictionReality)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        { 
            base.Apply(target, dest);

            try
            {

                var Things = DataOf.GetCachedThingList(Props.thingLists);

                if (Things.Count == 0)
                {
                    return;
                }

                ThingDef newItem = Things.RandomElement();

                // 生成新物品并放置在施法者周边
                var ok = GenPlace.TryPlaceThing(ThingMaker.MakeThing(newItem), Caster.Position, Caster.Map, ThingPlaceMode.Near);
                if (!ok)
                {
                    Messages.Message("NzRI_Zw_FictionReality_NoPlace".Translate(), MessageTypeDefOf.RejectInput);
                    return;
                }
                Messages.Message("NzRI_Zw_FictionReality_Success".Translate(newItem.label.Named("Thing")), MessageTypeDefOf.PositiveEvent);
            }
            catch (System.Exception e)
            {
                Log.Error("CompAbilityEffect_FictionReality.Apply: " + e);
            }
        }
    }
}