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

        public override bool GizmoDisabled(out string reason)
        {
            if (!Caster.HasEnoughFeiGang(Props.change))
            {
                reason = "NzRI_Zw_ReduceFg_NotEnough".Translate();
                return true;
            }

            return base.GizmoDisabled(out reason);
        }

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

            Caster.ChangeFeiGang(addFg);
            
        }
    }
}