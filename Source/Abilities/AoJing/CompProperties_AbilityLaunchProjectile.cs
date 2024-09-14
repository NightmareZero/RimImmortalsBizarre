using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{

    public class CompProperties_AbilityLaunchProjectile : CompProperties_AbilityEffect
    {
        public ThingDef projectileDef;

        // 技能教派
        public int skillRoute = 0;

        public CompProperties_AbilityLaunchProjectile()
        {
            compClass = typeof(CompAbilityEffect_LaunchProjectile);
        }
    }
}