using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    // WhoXiuXian.Abilities.CompProperties__ReduceEnergy

    public class CompProperties_Zw_AddYq : CompProperties_AbilityEffect
    {
        // 默认增加的非罡
        public int change;

        // 当前面有失败时的非罡
        public int onFail;

        public CompProperties_Zw_AddYq()
        {
            compClass = typeof(CompAbilityEffect_Zw_AddYq);
        }

    }

}