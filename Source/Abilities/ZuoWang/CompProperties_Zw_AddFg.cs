using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    // WhoXiuXian.Abilities.CompProperties__ReduceEnergy

    public class CompProperties_Zw_AddFg : CompProperties_AbilityEffect,IsCastingFailable
    {
        // 默认增加的非罡
        public int change;

        // 当前面有失败时的非罡
        public int onFail;

        // 是否非罡足够
        public bool enough = true;

        public CompProperties_Zw_AddFg()
        {
            compClass = typeof(CompAbilityEffect_Zw_AddFg);
        }

        public bool IsCastingSuccess()
        {
           return enough;
        }

    }

}