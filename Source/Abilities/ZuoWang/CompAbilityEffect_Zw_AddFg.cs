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

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            // var targetPawn = target.Pawn;
            // if (targetPawn == null)
            // {
            //     return;
            // }

            // var energyRoot = targetPawn?.health?.hediffSet?.GetFirstHediff<Hediff_RI_EnergyRoot>();
            // if (energyRoot == null)
            // {
            //     Log.Error("NzRI_ReduceFg_EnergyRootNotExist" + targetPawn.Name);
            //     return;
            // }

            // energyRoot.energy.ChangeEnergy(-Props.change);
            base.Apply(target, dest);
        }
    }
}