
using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    public class CompAbilityEffect_LaunchProjectile : CompAbilityEffect, DamagePatcher
    {
        public new CompProperties_AbilityLaunchProjectile Props => (CompProperties_AbilityLaunchProjectile)props;

        private float damageMultiplier = 1f;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Patcher.DoDamagePatch(this,this);
            LaunchProjectile(target);
        }

        private void LaunchProjectile(LocalTargetInfo target)
        {
            if (Props.projectileDef != null)
            {
                Pawn pawn = parent.pawn;
                Projectile projectile = (Projectile)GenSpawn.Spawn(Props.projectileDef, pawn.Position, pawn.Map);
                
                projectile.Launch(pawn, pawn.DrawPos, target, target, ProjectileHitFlags.IntendedTarget);
            }
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return target.Pawn != null;
        }

        public int PatchType()
        {
            return Props.skillRoute;
        }

        public void SetDamageMultiplier(float multiplier)
        {
            damageMultiplier = multiplier;
        }

        public void SetArmorPenetrationMultiplier(float penetration)
        {
            
        }
    }
}