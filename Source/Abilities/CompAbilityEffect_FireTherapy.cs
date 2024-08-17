using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;

namespace NzRimImmortalBizarre
{
    // CompAbilityEffect_Coagulate

    public class CompAbilityEffect_FireTherapy : CompAbilityEffect
    {
        private new CompProperties_FireTherapy Props => (CompProperties_FireTherapy)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return;
            }
        
            int num = 0;
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            for (int num2 = hediffs.Count - 1; num2 >= 0; num2--)
            {
                Hediff_Injury injury = hediffs[num2] as Hediff_Injury;
                Hediff_MissingPart missingPart = hediffs[num2] as Hediff_MissingPart;
        
                if ((injury != null || missingPart != null) && hediffs[num2].TendableNow())
                {
                    if (injury != null && injury.Part != null)
                    {
                        // 将受伤程度减半
                        injury.Heal(injury.Severity * 0.5f);
        
                        // 移除原伤口
                        pawn.health.RemoveHediff(injury);
        
                        // 将伤口类型修改为烧伤
                        Hediff_Injury burn = (Hediff_Injury)HediffMaker.MakeHediff(XmlOf.Burn, pawn, injury.Part);
                        burn.Severity = injury.Severity * 0.5f;
                        pawn.health.AddHediff(burn);
        
                        burn.Tended(Props.tendQualityRange.RandomInRange, Props.tendQualityRange.TrueMax, 1);
                    }
                    else if (missingPart != null)
                    {
                        // 仅包扎好缺失部位的伤口
                        missingPart.Tended(Props.tendQualityRange.RandomInRange, Props.tendQualityRange.TrueMax, 1);
                    }
        
                    num++;
                }
            }
        
            if (num > 0)
            {
                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "NumWoundsTended".Translate(num), 3.65f);
            }
        
            FleckMaker.AttachedOverlay(pawn, FleckDefOf.FlashHollow, Vector3.zero, 1.5f);
        }

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                AbilityUtility.ValidateHasTendableWound(pawn, throwMessages, parent);
            }

            return base.Valid(target, throwMessages);
        }
    }

}