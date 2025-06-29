using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;



namespace NzRimImmortalBizarre
{ 
    public class CompProperties_CopyTarget : CompProperties_AbilityEffect
    {
        public bool allowHuman = true; // 是否允许复制人类

        public bool allowAnimal = true; // 是否允许复制动物

        public bool allowMech = true; // 是否允许复制机械

        public bool allowBoss = false; // 是否允许复制Boss

        public bool allowItem = true; // 是否允许复制物品

        public HediffDef TargetHediff; // 施法者的状态

        public ThoughtDef TargetMind; // 施法者的想法

        public HediffDef NewPawnHediff; // 目标的状态

        public ThoughtDef NewPawnMind; // 目标的想法

        public bool copyJunk = false;  // 复制体是残次品


        public CompProperties_CopyTarget()
        {
            compClass = typeof(CompAbilityEffect_CopyTarget);
        }

    }
}