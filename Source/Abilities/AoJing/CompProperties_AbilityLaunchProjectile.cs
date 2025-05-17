using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{

    public class CompProperties_AbilityLaunchProjectile : CompProperties_AbilityEffect
    {
        public ThingDef projectileDef;

        public StatDef damageMultiplier;

        public CompProperties_AbilityLaunchProjectile()
        {
            compClass = typeof(CompAbilityEffect_LaunchProjectile);
        }
    }
}