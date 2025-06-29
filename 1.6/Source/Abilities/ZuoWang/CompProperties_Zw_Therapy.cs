using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_Zw_Therapy : CompProperties_AbilityEffect
    {
        // 复活
        public bool canRevive = false;

        // 治疗次数
        public int therapyTimes = 1;

        public CompProperties_Zw_Therapy()
        {
            compClass = typeof(CompAbilityEffect_Zw_Therapy);
        }
    }
}