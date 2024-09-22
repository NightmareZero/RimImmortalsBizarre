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

        // public override bool CanApplyOn(LocalTargetInfo target, LocalTargetInfo dest)
        // {
        //     if (dest.Pawn == null)
        //     {
        //         return false;
        //     }
        //     Log.Message("CompAbilityEffect_Zw_MvIllness.CanApplyOn");
        //     return true;
        // }

        private bool preApply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target == null || target.Pawn == null)
            {
                #if DEBUG
                Log.Warning("CompAbilityEffect_Zw_MvIllness.preApply: target.Pawn is null");
                #endif
                return false;
            }
            if (dest == null || dest.Pawn == null)
            {
                #if DEBUG
                Log.Warning("CompAbilityEffect_Zw_MvIllness.preApply: dest.Pawn is null");
                Messages.Message("NzRI_Zw_MvIllness_NoPerson".Translate(), MessageTypeDefOf.RejectInput);
                #endif
                return false;
            }
        
            return true;
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (!preApply(target, dest))
            {
                return;
            }
            #if DEBUG
            Log.Message("CompAbilityEffect_Zw_MvIllness.Apply: " + target.Pawn.Name +" to " + dest.Pawn.Name);
            #endif
            
            
            
            
        }
    }
}