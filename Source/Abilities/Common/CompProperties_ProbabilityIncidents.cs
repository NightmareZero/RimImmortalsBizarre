using RimWorld;
using System;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompProperties_ProbabilityIncidents : CompProperties_AbilityEffect
    {
        // 概率
        public int probability;

        // 概率上限
        public int maxProbability;

        public IncidentDef incidentDef;

        public CompProperties_ProbabilityIncidents()
        {
            compClass = typeof(CompAbilityEffect_ProbabilityIncidents);
        }
    }
}