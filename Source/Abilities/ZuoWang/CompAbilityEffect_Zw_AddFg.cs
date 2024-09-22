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

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (!base.Valid(target, throwMessages))
            {
                return false;
            }

            bool enough = Caster.HasEnoughFeiGang(Props.change);
            if (!enough)
            {
                Messages.Message("NzRI_Zw_ReduceFg_NotEnough".Translate(), MessageTypeDefOf.RejectInput);
                return false;
            }

            return true;
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