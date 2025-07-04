using Verse;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using Verse.Noise;
using Verse.Sound;
using System;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_MvIllness : CompAbilityEffect_WithDest
    {
        public new CompProperties_Zw_MvIllness Props => (CompProperties_Zw_MvIllness)props;

        private new Pawn Caster => parent.pawn;

        private bool preApply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target == null || target.Pawn == null)
            {
#if DEBUG
                Log.Warning("CompAbilityEffect_Zw_MvIllness.preApply: target.Pawn is null");
#endif
                return false;
            }
            if (dest == null || dest.Pawn == null)
            {
#if DEBUG
                Log.Warning("CompAbilityEffect_Zw_MvIllness.preApply: dest.Pawn is null");
                Messages.Message("NzRI_Zw_MvIllness_NoPerson".Translate(), MessageTypeDefOf.RejectInput);
#endif
                return false;
            }

            return true;
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            try
            {
                if (!preApply(target, dest))
                {
                    return;
                }
#if DEBUG
                Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: " + target.Pawn.Name + " to " + dest.Pawn.Name);
#endif

                // 获得一处待治愈的
#if DEBUG
                Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: Trying to get worst health condition");
#endif
                var get = Utils.TryGetWorstHealthCondition(target.Pawn, out Hediff worstHealth, out var worstBodyPart);
                if (!get)
                {
                    Messages.Message("NzRI_Zw_MvIllness_NoIllness".Translate(), MessageTypeDefOf.RejectInput);
                    return;
                }

                if (worstHealth != null)
                {
                    // 移除原来的疾病
#if DEBUG
                    Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: Curing worst health condition:" + worstHealth.def.label.Translate());
#endif
                    HealthUtility.Cure(worstHealth);

                    // 在目标身上添加一个相同的疾病
                    worstHealth.pawn = dest.Pawn;
                    dest.Pawn.health.AddHediff(worstHealth, worstBodyPart);
                }
                else { 
                    // 修复身体部位
#if DEBUG
                    Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: Fixing body part:" + worstBodyPart.def.label.Translate());
#endif
                    target.Pawn.health.RestorePart(worstBodyPart);

                    Utils.RemoveBodyPart(dest.Pawn, worstBodyPart);
                }


                // 播放特效
                dest.Pawn.TakeDamage(new DamageInfo(DamageDefOf.Vaporize, 1, 999f, -1f, null));
                XmlOf.NzRI_HurtSelf.PlayOneShot(new TargetInfo(Caster.Position, Caster.Map));

                if (!this.GetCastSuccess())
                {
#if DEBUG
                    Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: Cast failed, starting mental state");
#endif
                    dest.Pawn.mindState.mentalStateHandler
                    .TryStartMentalState(MentalStateDefOf.Berserk, reason: this.parent.def.label.Translate());
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.PrintExceptionWithStackTrace();
                return;
            }
        }


    }
}