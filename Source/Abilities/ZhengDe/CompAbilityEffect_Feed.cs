using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_Feed : CompAbilityEffect
    {
        public new CompProperties_Feed Props => (CompProperties_Feed)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = target.Pawn;
            if (pawn != null)
            {
                // 补满饥饿
                if (pawn.needs.food != null)
                {
                    pawn.needs.food.CurLevel = pawn.needs.food.MaxLevel;
                }

                // 补满睡眠
                if (pawn.needs.rest != null)
                {
                    pawn.needs.rest.CurLevel = pawn.needs.rest.MaxLevel;
                }

                // 将目标的成瘾品需求补满
                if (pawn.story != null)
                {
                    foreach (var hediff in pawn.health.hediffSet.hediffs)
                    {
                        if (hediff.def.IsAddiction)
                        {
                            hediff.Severity = 0;
                        }
                    }
                }
            }
        }
    }
}