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

        public Zd_Fruition _zdFruitionCache = null;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        { 
            base.Apply(target, dest);
            if (_zdFruitionCache == null)
            {
                _zdFruitionCache = Utils.AssertGetFruitionHediff(caster);
            }

            // 添加佛性
            _zdFruitionCache.Tracker.addBuddhaNature();
            
            
        }
    }
}