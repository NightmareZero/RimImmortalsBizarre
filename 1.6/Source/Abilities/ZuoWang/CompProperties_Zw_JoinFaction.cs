using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{ 
    public class CompProperties_Zw_JoinFaction : CompProperties_AbilityEffect
    {
        // 默认增加的非罡
        public bool chIdeo = false;

        public CompProperties_Zw_JoinFaction()
        {
            compClass = typeof(CompAbilityEffect_Zw_JoinFaction);
        }

    }
}