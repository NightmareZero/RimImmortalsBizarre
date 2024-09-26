using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{ 
    public class CompAbilityEffect_Zw_Therapy : CompAbilityEffect
    {
        public new CompProperties_Zw_Therapy Props => (CompProperties_Zw_Therapy)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            var targetPawn = target.Pawn;
            if (targetPawn == null)
            {
                targetPawn = ((Corpse)target.Thing).InnerPawn;
                if (targetPawn == null)
                {
                    return;
                }
            }

            if (Props.canRevive)
            {
                if (targetPawn.Dead)
                {
                    ResurrectionUtility.TryResurrectWithSideEffects(targetPawn);
                }
            }

            if (Props.therapyTimes > 0)
            {
                for (int i = 0; i < Props.therapyTimes; i++)
                {
                    targetPawn.health.hediffSet.hediffs.Where(hediff => hediff.def.isBad).RandomElement()?.Heal(1);
                }
            }
        }
    }

    // 参考机械液
    // CompProperties_TargetEffectResurrect
    // CompProperties_Resurrect
}