
using Verse;
using RimWorld;
using UnityEngine;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class ProjectileExt : Projectile
    {
        public StatDef DamageAmountMultiplier;

        public override void Launch(Thing launcher, Vector3 origin, LocalTargetInfo usedTarget, LocalTargetInfo intendedTarget, ProjectileHitFlags hitFlags, bool preventFriendlyFire = false, Thing equipment = null, ThingDef targetCoverDef = null)
        {
            this.launcher = launcher;
            this.origin = origin;
            this.usedTarget = usedTarget;
            this.intendedTarget = intendedTarget;
            this.targetCoverDef = targetCoverDef;
            this.preventFriendlyFire = preventFriendlyFire;
            HitFlags = hitFlags;
            if (equipment != null)
            {
                equipmentDef = equipment.def;
                weaponDamageMultiplier = equipment.GetStatValue(StatDefOf.RangedWeapon_DamageMultiplier);
                equipment.TryGetQuality(out equipmentQuality);
            }
            else if (DamageAmountMultiplier != null)
            {
                equipmentDef = null;
                weaponDamageMultiplier = launcher.GetStatValue(DamageAmountMultiplier);
            }
            else
            {
                equipmentDef = null;
                weaponDamageMultiplier = 1f;
            }

            destination = usedTarget.Cell.ToVector3Shifted() + Gen.RandomHorizontalVector(0.3f);
            ticksToImpact = Mathf.CeilToInt(StartingTicksToImpact);
            if (ticksToImpact < 1)
            {
                ticksToImpact = 1;
            }

            lifetime = ticksToImpact;
            // if (!def.projectile.soundAmbient.NullOrUndefined())
            // {
            //     ambientSustainer = def.projectile.soundAmbient.TrySpawnSustainer(SoundInfo.InMap(this, MaintenanceType.PerTick));
            // }
        }
    }
}