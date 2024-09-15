using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_GoodImpression : CompAbilityEffect_ZuoWangBase
    {
        public new CompProperties_GoodImpression Props => (CompProperties_GoodImpression)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            var targetPawn = target.Pawn;
            if (targetPawn == null || targetPawn == Caster)
            {
                return;
            }


            if (GetCastSuccess())
            {
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(Props.onSuccess, Caster);
            }
            else
            {
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(Props.onFail, Caster);
            }
            base.Apply(target, dest);
        }
    }
}