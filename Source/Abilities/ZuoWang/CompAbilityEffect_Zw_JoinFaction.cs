using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Zw_JoinFaction : CompAbilityEffect
    {
        public new CompProperties_Zw_JoinFaction Props => (CompProperties_Zw_JoinFaction)props;

        private Pawn Caster => parent.pawn;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            // defaultProp();
            var targetPawn = target.Pawn;
            if (targetPawn == null || targetPawn == Caster)
            {
                return;
            }

            // 目标已经是本方的
            if (targetPawn.Faction == Caster.Faction)
            {
                return;
            }

            if (this.GetCastSuccess())
            {
                if (Props.chIdeo)
                {
                    // 直接替换目标的文化
                    targetPawn.ideo = Caster.ideo; // 替换目标的文化
                }
                // 目标加入本方
                targetPawn.SetFaction(Caster.Faction);
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(XmlOf.NzRI_SaveMe, Caster);
            }
            else
            {
                Messages.Message("NzRI_Zw_Is_Cheater".Translate(targetPawn.Name.Named("Pawn"), Caster.Name.Named("Caster")), MessageTypeDefOf.RejectInput);
                targetPawn.needs.mood.thoughts.memories.TryGainMemory(XmlOf.NzRI_CheatMe, Caster);
            }
        }

    }
}