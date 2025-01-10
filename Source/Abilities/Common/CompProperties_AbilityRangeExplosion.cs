using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{

    
    public class CompProperties_AbilityRangeExplosion : CompProperties_AbilityEffect
    {

        // 目标误差范围
        public float deviationRange;

        // 单次爆炸范围
        public float explosionRange;

        // 遗留地板类型
        public ThingDef filthDef;

        // 伤害类型(多种)
        public List<DamageDef> damageTypes = new List<DamageDef>() ;

        // 伤害类型
        public DamageDef damageType;

        // 伤害次数
        public int damageTimes = 1;

        // 伤害值
        public int damage = 40;

        // 穿甲值
        public float armorPenetration = 1f;

        // 伤害放大系数
        public StatDef damageFactorStat;

        // 穿甲放大系数
        public StatDef armorPenetrationFactorStat;

        // 爆炸特效
        public EffecterDef effecterDef;

        // 爆炸后音效
        public SoundDef explosionSound = XmlOf.Explosion_GiantBomb;

        // 给受到影响的人添加hediff
        public HediffDef addHediffDef;

        // 给受到影响的人添加hediff
        public List<HediffDef> addHediffDefs = new List<HediffDef>();

        public CompProperties_AbilityRangeExplosion()
        {
            compClass = typeof(CompAbilityEffect_RangeExplosion);
        }
    }
}