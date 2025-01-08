using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Hit : CompAbilityEffect
    {
        public new CompProperties_Hit Props => (CompProperties_Hit)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (!canDo(target))
            {
                return;
            }

            // 计算伤害和穿甲
            var damAmount = Props.damageAmountBase;
            if (Props.damageMultiplierStat != null)
            {
                damAmount = Mathf.RoundToInt(damAmount * parent.pawn.GetStatValue(Props.damageMultiplierStat));
            }
            var damArmorPenetration = Props.armorPenetrationBase;
            if (Props.armorPenetrationMultiplierStat != null)
            {
                damArmorPenetration = Mathf.RoundToInt(damArmorPenetration * parent.pawn.GetStatValue(Props.armorPenetrationMultiplierStat));
            }

            // 造成伤害
            DamageInfo damageInfo = new DamageInfo(Props.damageDef, damAmount, damArmorPenetration, -1, this.parent.pawn, null, null, DamageInfo.SourceCategory.ThingOrUnknown, target.Thing);
            target.Pawn.TakeDamage(damageInfo);

            // 击晕
            if (Props.stunTicks > 0)
            {
                target.Pawn.stances.stunner.StunFor(Props.stunTicks, parent.pawn);
            }

            // 播放打击音效
            if (Props.soundHitPawn != null)
            {
                Props.soundHitPawn.PlayOneShot(target.Pawn);
            }
        }

        public bool canDo(LocalTargetInfo target)
        {
            if (target.Pawn == null)
            {
                return false;
            }

            if (Props.damageDef == null)
            {
                Props.damageDef = DamageDefOf.Blunt;
            }

            if (Props.damageAmountBase < 1)
            {
                return false;
            }

            if (Props.armorPenetrationBase < 0)
            {
                return false;
            }

            return true;
        }
    }
}