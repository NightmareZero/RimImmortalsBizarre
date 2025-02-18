using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{


    public class CompProperties_AbilityRangeHediff : CompProperties_AbilityEffect
    {

        // 作用范围范围
        public float range;

        // 生成Hediff
        public HediffDef hediffDef;

        // 删除而不是添加
        public bool del = false;

        // 特效mote(每个人)
        public ThingDef effectMote;

        public CompProperties_AbilityRangeHediff()
        {
            compClass = typeof(CompAbilityEffect_RangeHediff);
        }
    }
}