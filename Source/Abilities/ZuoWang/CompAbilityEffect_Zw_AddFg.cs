using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_AddFg : CompAbilityEffect
    {
        private new CompProperties_Zw_AddFg Props => (CompProperties_Zw_AddFg)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            #if DEBUG
            Log.Message("CompAbilityEffect_Zw_AddFg.Apply");
            #endif

            // 计算变更的非罡值
            var addFg = Props.change;
            if (!this.GetCastSuccess())
            {
                addFg = Props.onFail;
            }

            bool enough = Caster.ChangeFeiGang(addFg);
            if (!enough)
            {
                Messages.Message("NzRI_Zw_ReduceFg_NotEnough".Translate(), MessageTypeDefOf.RejectInput);
                Props.enough = false;
                return;
            }

            
        }
    }
}