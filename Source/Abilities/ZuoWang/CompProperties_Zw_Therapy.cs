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

        // 能否治疗成瘾
        public bool canAddiction = false;
    }
}