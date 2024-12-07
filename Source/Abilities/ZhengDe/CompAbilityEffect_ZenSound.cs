using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_ZenSound : CompAbilityEffect { 
        public new CompProperties_ZenSound Props => (CompProperties_ZenSound)props;

        public Pawn caster => parent.pawn;

        public Zd_Fruition zdFruitionCache = null;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        { 
            base.Apply(target, dest);
            if (zdFruitionCache == null)
            {
                zdFruitionCache = Utils.GetFruitionHediff(caster);
            }
            
            
        }
    }
}