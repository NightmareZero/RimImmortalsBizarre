using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_AddYq : CompAbilityEffect
    {
        private new CompProperties_Zw_AddYq Props => (CompProperties_Zw_AddYq)props;

        private Pawn Caster => parent.pawn;

        public override bool GizmoDisabled(out string reason)
        {
            if (!Caster.HasEnoughYiQi(Props.change))
            {
                reason = "NzRI_Zw_ReduceYq_NotEnough".Translate();
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

            try
            {
                // 计算变更的非罡值
                var yqChange = Props.change;
                Log.Message($"yqChange: {yqChange}");
                if (!this.GetCastSuccess())
                {
                    yqChange = Props.onFail;
                }
                Log.Message($"yqChange: 2");

                Caster.ChangeYiQi(yqChange);
                Log.Message($"yqChange: 3");
            }
            catch (Exception ex)
            {
                Log.Error($"Error applying CompAbilityEffect_Zw_AddYq: {ex.Message}");
            }

        }
    }
}