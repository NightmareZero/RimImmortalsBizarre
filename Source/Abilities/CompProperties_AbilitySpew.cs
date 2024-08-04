using Verse;
using RimWorld;

namespace NzRimImmortalBizarre
{

    
    public class CompProperties_AbilitySpew : CompProperties_AbilityEffect
    {
        public float range;

        public float lineWidthEnd;

        public ThingDef filthDef;

        public DamageDef damageType;

        public int damAmount = -1;

        public EffecterDef effecterDef;

        public bool canHitFilledCells;

        public CompProperties_AbilitySpew()
        {
            compClass = typeof(CompAbilityEffect_Spew);
        }
    }
}