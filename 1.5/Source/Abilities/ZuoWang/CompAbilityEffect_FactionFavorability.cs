using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_FactionFavorability : CompAbilityEffect
    {
        public new CompProperties_FactionFavorability Props => (CompProperties_FactionFavorability)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            var targetPawn = target.Pawn;
            if (targetPawn == null || targetPawn == Caster)
            {
                return;
            }
            // 如果目标的派系和施法者的派系相同，直接返回
            if (targetPawn.Faction == Caster.Faction)
            {
                return;
            }

            if (this.GetCastSuccess())
            {
                targetPawn.Faction.TryAffectGoodwillWith(Caster.Faction, Props.onSuccess);
            }
            else
            {
                targetPawn.Faction.TryAffectGoodwillWith(Caster.Faction, Props.onFail);
            }
            base.Apply(target, dest);
        }
    }
}