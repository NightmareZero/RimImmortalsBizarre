using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_AddFg : CompAbilityEffect_ZuoWangBase
    {
        private new CompProperties_Zw_AddFg Props => (CompProperties_Zw_AddFg)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            bool enough = Caster.ChangeFeiGang(Props.change);
            if (!enough)
            {
                Messages.Message("NzRI_Zw_ReduceFg_NotEnough".Translate(), MessageTypeDefOf.RejectInput);
                Props.enough = false;
                return;
            }

            base.Apply(target, dest);
        }
    }
}