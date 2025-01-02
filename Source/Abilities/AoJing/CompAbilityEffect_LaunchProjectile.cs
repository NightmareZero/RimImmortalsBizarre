
using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    public class CompAbilityEffect_LaunchProjectile : CompAbilityEffect
    {
        public new CompProperties_AbilityLaunchProjectile Props => (CompProperties_AbilityLaunchProjectile)props;

        // TODO 修改技能伤害
        private float damageMultiplier = 1f;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            LaunchProjectile(target);
        }

        private void LaunchProjectile(LocalTargetInfo target)
        {
            if (Props.projectileDef != null)
            {
                Pawn pawn = parent.pawn;
                Projectile projectile = (Projectile)GenSpawn.Spawn(Props.projectileDef, pawn.Position, pawn.Map);
                // TODO 修改技能伤害
                
                projectile.Launch(pawn, pawn.DrawPos, target, target, ProjectileHitFlags.IntendedTarget);
            }
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return target.Pawn != null;
        }

    }
}