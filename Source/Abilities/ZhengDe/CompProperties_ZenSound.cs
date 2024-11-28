using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompProperties_ZenSound : AbilityCompProperties
    { 

        public CompProperties_ZenSound()
        {
            compClass = typeof(CompAbilityEffect_ZenSound);
        }
    }
}