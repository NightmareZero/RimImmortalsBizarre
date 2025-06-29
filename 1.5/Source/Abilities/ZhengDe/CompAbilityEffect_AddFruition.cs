using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_AddFruition : CompAbilityEffect
    {
        public new CompProperties_AddFruition Props => (CompProperties_AddFruition)props;

        public Pawn caster => parent.pawn;

        public Zd_Fruition _zdFruitionCache = null;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (_zdFruitionCache == null)
            {
                _zdFruitionCache = Utils.AssertGetFruitionHediff(caster);
            }

            if (Props.buddha)
            {
                // 添加佛性
                _zdFruitionCache.Tracker.addBuddhaNature();
            }

            if (Props.sacrifice)
            {
                // 添加舍身
                _zdFruitionCache.Tracker.addSelfSacrifice();
            }
        }
    }
}