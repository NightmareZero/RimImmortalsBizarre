using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    // CompProperties_AbilitySocialInteraction
    public class CompAbilityEffect_GoodImpression : CompAbilityEffect
    {
        public new CompProperties_GoodImpression Props => (CompProperties_GoodImpression)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {

            propDefault();
            var targetPawn = target.Pawn;
            if (targetPawn == null || targetPawn == Caster)
            {
                Log.Error("need a non caster target");
                return;
            }

            if (targetPawn.needs?.mood?.thoughts?.memories == null)
            {
                Log.Error("targetPawn.needs.mood.thoughts.memories is null");
                return;
            }

            if (this.GetCastSuccess())
            {
                // 添加对应的记忆
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(Props.onSuccess, Caster);
                if (targetPawn != null && Caster != targetPawn)
                {
                    Caster.interactions?.TryInteractWith(targetPawn, Thought1Def.WordOfJoy);
                }
            }
            else
            {
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(Props.onFail, Caster);
                if (targetPawn != null && Caster != targetPawn)
                {
                    Caster.interactions?.TryInteractWith(targetPawn, Thought1Def.Slight);
                }
            }
            base.Apply(target, dest);
        }

        private void propDefault()
        {
            // 一些默认值
            if (Props.onSuccess == null)
            {
                Props.onSuccess = Thought1Def.NzRI_T_GracefulWords;
            }
            if (Props.onFail == null)
            {
                Props.onFail = Thought1Def.NzRI_T_NonsenseWords;
            }

        }
    }
}