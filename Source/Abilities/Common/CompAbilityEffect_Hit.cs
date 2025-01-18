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

            // 击晕
            if (Props.stunTicks > 0)
            {
                target.Pawn.stances.stunner.StunFor(Props.stunTicks, parent.pawn);
            }

            // 击打部位
            BodyPartRecord hitPartRcord = null;
            if (Props.hitPart != null)
            {
                hitPartRcord = target.Pawn.health.hediffSet.GetNotMissingParts().FirstOrDefault(x => x.def == Props.hitPart);
            }


            // 造成伤害
            DamageInfo damageInfo = new DamageInfo(Props.DamageDef, damAmount, damArmorPenetration, -1, this.parent.pawn, hitPartRcord, null, DamageInfo.SourceCategory.ThingOrUnknown, target.Thing);
            target.Pawn.TakeDamage(damageInfo);


            // 播放打击音效
            Props.SoundHitPawn.PlayOneShot(target.Pawn);
        }

        public bool canDo(LocalTargetInfo target)
        {
            if (target.Pawn == null)
            {
                return false;
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