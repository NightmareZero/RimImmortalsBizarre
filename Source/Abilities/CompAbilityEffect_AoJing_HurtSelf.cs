using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_AoJing_HurtSelf : CompAbilityEffect
    {

        public new CompProperties_AoJing_HurtSelf Props => (CompProperties_AoJing_HurtSelf)props;

        public override void Initialize(AbilityCompProperties props)
        {
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
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return;
            }
            Pawn caster = parent.pawn;
            caster.DamageBodyPart(Props.group, Props.level, Props.damageMin, Props.damageMax);

            
            
            
            // BodyPartRecord bodyPartRecord = pawn.health.hediffSet.GetNotMissingParts().RandomElementByWeight((BodyPartRecord x) => x.coverageAbs);
            // if (bodyPartRecord == null)
            // {
            //     return;
            // }
            // pawn.TakeDamage(new DamageInfo(DamageDefOf.SurgicalCut, 99999f, 999f, -1f, null, bodyPartRecord));
        }
    }
}