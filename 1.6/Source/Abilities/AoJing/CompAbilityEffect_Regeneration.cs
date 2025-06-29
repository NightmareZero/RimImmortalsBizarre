using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Regeneration : CompAbilityEffect
    {

        private new CompProperties_Regeneration Props => (CompProperties_Regeneration)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn == null)
            {
                return;
            }

            if (Props.times > 0)
            {
                for (int i = 0; i < Props.times; i++)
                {
                    // 恢复生命
                    HealthUtility.FixWorstHealthCondition(pawn);
                }
            }
        }

    }
}