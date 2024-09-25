using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_InvokeDeity : CompAbilityEffect_AreaAddHediff {
        private CompProperties_AreaAddHediff Props => (CompProperties_AreaAddHediff)props;

        public override List<Pawn> SelectPawn()
        {
            return base.SelectPawn();
        }
    }
}