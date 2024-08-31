using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Ascend : CompAbilityEffect
    {
        private new CompProperties_AbilityAscend Props => (CompProperties_AbilityAscend)props;


        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);

        }
    }
}