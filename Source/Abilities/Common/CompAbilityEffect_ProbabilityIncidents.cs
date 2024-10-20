using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_ProbabilityIncidents : CompAbilityEffect
    {
        public new CompProperties_ProbabilityIncidents Props => (CompProperties_ProbabilityIncidents)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (Props.incidentDef == null)
            {
                return;
            }

            if (Props.probability < 1)
            {
                return;
            }

            if (Props.maxProbability < 1 || Props.maxProbability < Props.probability)
            {
                return;
            }


            if (MathUtil.IsProbability(Props.probability, Props.maxProbability))
            {
                // 一段时间后触发事件 Nz_XinSuWandersIn
                IncidentParms parms = new IncidentParms();
                parms.target = Find.CurrentMap;
                // 设置事件触发时间，10秒到180秒后
                int delayTicks = Rand.Range(600, 10800);
                Find.Storyteller.incidentQueue.Add(Props.incidentDef, Find.TickManager.TicksGame + delayTicks, parms);
            }
        }
    }
}