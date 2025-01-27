using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;
using Verse.Sound;
using Core;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_ReduceEnergy : CompAbilityEffect
    {
        public new CompProperties_ReduceEnergy Props => (CompProperties_ReduceEnergy)props;

        public Pawn Caster => parent.pawn;

        public override bool ShouldHideGizmo => Caster.GetRIRoot() == null;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            var er = Caster.GetRIRoot();
            if (er == null)
            {
                return;
            }
            er?.energy.SetEnergy(Props.rEnergy);
            er?.energy.SetExp(0.3f * (-Props.rEnergy));
        }

        public override bool GizmoDisabled(out string reason)
        {
            var er = Caster.GetRIRoot();
            // ShouldHideGizmo, 所以不会显示
            if (er == null)
            {
                reason = "";
                return true;
            }
            if (er.energy.Energy < -Props.rEnergy)
            {
                reason = "NzRI_CompAbilityEffect_ReduceEnergy_NotEnough".Translate();
                return true;
            }
            reason = null;
            return false;
        }
    }
}