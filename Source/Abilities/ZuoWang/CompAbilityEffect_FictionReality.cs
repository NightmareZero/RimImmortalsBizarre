using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_FictionReality : CompAbilityEffect
    {
        // CompProperties_AbilityChunkskip
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
                Thing itemIns = ThingMaker.MakeThing(newItem);

                // 生成新物品并放置在施法者周边
                var ok = GenPlace.TryPlaceThing(itemIns, Caster.Position, Caster.Map, ThingPlaceMode.Near);
                if (!ok)
                {
                    Messages.Message("NzRI_Zw_FictionReality_NoPlace".Translate(), MessageTypeDefOf.RejectInput);
                    return;
                }
                FleckMaker.Static(itemIns.Position, parent.pawn.Map, FleckDefOf.PsycastSkipFlashEntry, 0.72f);
                SoundDefOf.Psycast_Skip_Pulse.PlayOneShot(new TargetInfo(target.Cell, Caster.Map));
                Messages.Message("NzRI_Zw_FictionReality_Success".Translate(newItem.label.Named("Thing")), MessageTypeDefOf.PositiveEvent);
            }
            catch (System.Exception e)
            {
                Log.Error("CompAbilityEffect_FictionReality.Apply: " + e);
            }
        }
    }
}