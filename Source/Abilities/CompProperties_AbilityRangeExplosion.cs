using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{

    
    public class CompProperties_AbilityRangeExplosion : CompProperties_AbilityEffect
    {

        // 目标误差范围
        public float targetRange;

        // 单次爆炸范围
        public float explosionRange;

        // 遗留地板类型
        public ThingDef filthDef;

        // 伤害类型(多种)
        public List<DamageDef> damageTypes;

        // 伤害次数
        public int damageTimes = 1;

        // 伤害值
        public int damage = 40;

        // 技能教派
        public int skillRoute = 0;

        public EffecterDef effecterDef;

        public CompProperties_AbilityRangeExplosion()
        {
            compClass = typeof(CompAbilityEffect_RangeExplosion);
        }
    }
}