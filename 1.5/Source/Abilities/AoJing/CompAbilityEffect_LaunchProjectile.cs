
using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    public class CompAbilityEffect_LaunchProjectile : CompAbilityEffect
    {
        public new CompProperties_AbilityLaunchProjectile Props => (CompProperties_AbilityLaunchProjectile)props;

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
                Projectile projectile;

                if (Props.projectileDef.thingClass == typeof(ProjectileExt))
                {
                    ProjectileExt projectile1 = (ProjectileExt)GenSpawn.Spawn(Props.projectileDef, pawn.Position, pawn.Map);
                    if (Props.damageMultiplier != null)
                    {

                        projectile1.DamageAmountMultiplier = Props.damageMultiplier;


                    }
                    projectile = projectile1;
                }
                else
                {
                    projectile = (Projectile)GenSpawn.Spawn(Props.projectileDef, pawn.Position, pawn.Map);
                }


                projectile.Launch(pawn, pawn.DrawPos, target, target, ProjectileHitFlags.IntendedTarget);
            }
        }

    }
}