using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_Zw_CalcuCheatSuccess : CompProperties_AbilityEffect, IsCastingFailable
    {
        // 是否成功
        public bool cheatSuccess = false;

        public bool IsCastingSuccess()
        {
            return cheatSuccess;
        }
    }
}