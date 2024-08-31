using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Ascend : CompAbilityEffect
    {
        private new CompProperties_AbilityAscend Props => (CompProperties_AbilityAscend)props;



        public override Window ConfirmationDialog(LocalTargetInfo target, Action confirmAction)
        {
            Pawn pawn = target.Pawn;
            #if DEBUG
            Log.Message("NzRI_Ascend_Apply Pawn Mood : " + pawn.GetPawnMoods() + " Pawn Pain: "+ pawn.GetPawnPain() + ", SuccessRate: " + getSuccessRate(pawn));
            #endif
            float successRate = getSuccessRate(pawn);
            var message = "NzRI_WarningAscendSuccessRate".Translate(successRate.Named("SUCCESSRATE"));
            return Dialog_MessageBox.CreateConfirmation(message, confirmAction, destructive: true);
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;


        }

        private float getSuccessRate(Pawn pawn)
        {
            float successRate = 0.1f;
            successRate += (1-pawn.GetPawnMoods())/2;
            successRate += pawn.GetPawnPain()/2;
            if (successRate < 0.1f)
            {
                successRate = 0.1f;
            } else if (successRate > 0.9f)
            {
                successRate = 0.9f;
            }
            successRate *= 100;
            return successRate;
        }
    }
}