using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Collections.Generic;
using System.Linq;
using Verse.Noise;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_MvIllness : CompAbilityEffect_WithDest
    {
        public new CompProperties_Zw_MvIllness Props => (CompProperties_Zw_MvIllness)props;

        private Pawn Caster => parent.pawn;

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
            if (!preApply(target, dest))
            {
                return;
            }
#if DEBUG
            Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: " + target.Pawn.Name + " to " + dest.Pawn.Name);
#endif

            // 获得一处待治愈的
            var get = Utils.TryGetWorstHealthCondition(target.Pawn, out Hediff worstHealth, out var worstBodyPart);
            if (!get)
            {
                Messages.Message("NzRI_Zw_MvIllness_NoIllness".Translate(), MessageTypeDefOf.RejectInput);
                return;
            }

            // 移除原来的疾病
            HealthUtility.Cure(worstHealth);
            // 在目标身上添加一个相同的疾病
            worstHealth.pawn = dest.Pawn;
            dest.Pawn.health.AddHediff(worstHealth, worstBodyPart);

            // 播放特效
            dest.Pawn.TakeDamage(new DamageInfo(DamageDefOf.Vaporize, 1, 999f, -1f, null));
            XmlOf.NzRI_HurtSelf.PlayOneShot(new TargetInfo(Caster.Position, Caster.Map));

            if (!this.GetCastSuccess())
            {
                dest.Pawn.mindState.mentalStateHandler
                    .TryStartMentalState(MentalStateDefOf.Berserk, reason: this.parent.def.label.Translate());
                return;
            }

        }


    }
}