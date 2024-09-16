using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_CalcuCheatSuccess : CompAbilityEffect
    {
        private CompProperties_Zw_CalcuCheatSuccess _props;
    
        public new CompProperties_Zw_CalcuCheatSuccess Props
        {
            get => _props ?? (_props = (CompProperties_Zw_CalcuCheatSuccess)props);
            set => _props = value;
        }
    
        
        private Pawn Caster => parent.pawn;
    
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
          
        }

        public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Pawn == null || target.Pawn == Caster)
            {
                return false;
            }
        
            // 调用计算和设置成功结果的方法
            SetCheatSuccess(target.Pawn);
        
            return base.CanApplyOn(target, dest);
        }
        
        // 提取的计算和设置成功结果的方法
        private void SetCheatSuccess(Pawn targetPawn)
        {
            // 计算欺骗目标能否成功
            // 5 * 自己的社交 - 5 * 目标智力 + 目标对自己的总好感
            var successRate = 5 * Caster.skills.GetSkill(SkillDefOf.Social).Level - 5 * targetPawn.skills.GetSkill(SkillDefOf.Intellectual).Level + targetPawn.relations.OpinionOf(Caster);
        
            if (successRate < 0)
            {
                Props.cheatSuccess = false;
                return;
            }
        
            // 生成一个 0 - 100 的随机数，如果小于成功率则成功
            Props.cheatSuccess = Rand.Range(0, 100) < successRate;
        }
    }
}