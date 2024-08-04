using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_AoJing_HurtSelf : CompAbilityEffect
    {

        public new CompProperties_AoJing_HurtSelf Props => (CompProperties_AoJing_HurtSelf)props;

        public override void Initialize(AbilityCompProperties props)
        {
            // CompAbilityEffect_FireSpew
            base.Initialize(props);
        }

        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            // 判断自身是否有可消耗的身体部位
            var canDamage = parent.pawn.HasCanDamageBodyPart(Props.group, Props.level);
            // 输出消息
            if (!canDamage)
            {
                Messages.Message("Nz_AoJing_HurtSelf_NoBodyPart".Translate(parent.pawn.Name.ToStringShort), parent.pawn, MessageTypeDefOf.RejectInput);
            }
            return base.CanApplyOn(target, dest) &&  canDamage;
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn caster = parent.pawn;
            caster.DamageBodyPart(Props.group, Props.level, Props.damageMin, Props.damageMax);
        }
    }
}