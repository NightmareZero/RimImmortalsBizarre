using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;
using WhoXiuXian.Abilities;

using Core;
using WhoXiuXian;


namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Ascend : CompAbilityEffect
    {
        private new CompProperties_AbilityAscend Props => (CompProperties_AbilityAscend)props;



        public override Window ConfirmationDialog(LocalTargetInfo target, Action confirmAction)
        {
            Pawn pawn = target.Pawn;
#if DEBUG
            Log.Message("NzRI_Ascend_Apply Pawn Mood : " + pawn.GetPawnMoods() + " Pawn Pain: " + pawn.GetPawnPain() + ", SuccessRate: " + getSuccessRate(pawn));
#endif
            float moodRate = (1 - pawn.GetPawnMoods()) * 100;
            float painRate = (pawn.GetPawnPain()) * 100;
            float successRate = getSuccessRate(pawn) * 100;
            var message = "NzRI_WarningAscendSuccessRate".Translate(successRate.Named("Success"), painRate.Named("Pain"), moodRate.Named("Mood"));
            return Dialog_MessageBox.CreateConfirmation(message, confirmAction, destructive: true);
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn == null) return;
            
            // 生成一个 0f ~ 1f 的随机数，如果小于成功率则成功
            float successRate = getSuccessRate(pawn);
            float random = Rand.Value;
            if (random > successRate) // 失败
            {
                Log.Message("NzRI_Ascend_FailMsg".Translate(pawn.Named("Pawn")));
                return;
            }
            // 成功
            if (Utils.HasAscendHediff(pawn))
            {
                Hediff hediff = Utils.GetAscendHediff(pawn);
                if (hediff.Severity >= 0.5f) {
                    // 恢复满灵气
                    Hediff_RI_EnergyRoot energyRoot = pawn?.health?.hediffSet?.GetFirstHediff<Hediff_RI_EnergyRoot>();
                    if (energyRoot != null)
                    { 
                        Log.Error("NzRI_Ascend_EnergyRootNotExist" + pawn.Name);
                        return;
                    }
                    energyRoot.energy.SetEnergy(999f);
                    return; 
                }
                hediff.Severity += 0.1f;
            }
            else
            {
                Hediff hediff = HediffMaker.MakeHediff(XmlOf.NzRI_AscendHediff, pawn);
                hediff.Severity = 0.1f;
                pawn.health.AddHediff(hediff);
            }
            

        }

        private float getSuccessRate(Pawn pawn)
        {
            float successRate = 0.1f;
            successRate += (1 - pawn.GetPawnMoods()) / 2;
            successRate += pawn.GetPawnPain() / 2;
            if (successRate < 0.1f)
            {
                successRate = 0.1f;
            }
            else if (successRate > 0.9f)
            {
                successRate = 0.9f;
            }
            return successRate;
        }
    }
}