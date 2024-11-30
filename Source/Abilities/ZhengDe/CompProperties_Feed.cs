using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompProperties_Feed : AbilityCompProperties { 

        public CompProperties_Feed()
        {
            compClass = typeof(CompAbilityEffect_Feed);
        }
    }
}