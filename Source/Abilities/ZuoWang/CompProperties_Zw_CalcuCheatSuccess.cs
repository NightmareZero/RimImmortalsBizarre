using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_Zw_CalcuCheatSuccess : CompProperties_AbilityEffect, IsCastingFailable
    {
        // 是否成功
        public bool cheatSuccess = false;

        public int min = 0;

        public int max = 100;

        public bool IsCastingSuccess()
        {
            return cheatSuccess;
        }

        public CompProperties_Zw_CalcuCheatSuccess()
        {
            compClass = typeof(CompAbilityEffect_Zw_CalcuCheatSuccess);
        }
    }
}