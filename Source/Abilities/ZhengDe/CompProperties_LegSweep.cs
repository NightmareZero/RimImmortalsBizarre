using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    // CompProperties_AbilitySprayLiquid


    public class CompProperties_LegSweep : CompProperties_AbilityEffect
    {

        public float radius;

        public int damage;

        public StatDef damageMultiplierStat;

        public DamageDef damageDef;

        public int stunTicks;

        public float armorPenetration;

        public List<BodyPartDef> preferredAttackBodyParts;

        public CompProperties_LegSweep()
        {
            compClass = typeof(CompAbilityEffect_LegSweep);
        }
    }
}