using Verse;
using RimWorld;
using NzRimImmortalBizarre;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_MvIllness : CompAbilityEffect_WithDest
    {
        public new CompProperties_Zw_MvIllness Props => (CompProperties_Zw_MvIllness)props;

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            if (!base.Valid(target, throwMessages))
            {
                return false;
            }
            if (target.Pawn == null)
            {
                if (throwMessages)
                {
                    Messages.Message("AbilityInvalidTarget".Translate(), MessageTypeDefOf.RejectInput);
                }
                return false;
            }
            return true;
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Pawn == null)
            {
                
                return;
            }
            // 目标地点
            LocalTargetInfo extraTarget = this.selectedTarget;
            if (extraTarget == null || extraTarget.Pawn == null)
            {
                // 如果没有指定目标地点，或者目标地点没有人，直接消息错误
                return;
            }
            
            
        }
    }
}