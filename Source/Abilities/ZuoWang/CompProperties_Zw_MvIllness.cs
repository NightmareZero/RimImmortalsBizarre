using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_Zw_MvIllness :  CompProperties_EffectWithDest
    {

        public CompProperties_Zw_MvIllness()
        {
            compClass = typeof(CompAbilityEffect_Zw_MvIllness);
            destination = AbilityEffectDestination.Selected; // 选择目标
            requiresLineOfSight = true; // 需要视线
            
        }
    }
}