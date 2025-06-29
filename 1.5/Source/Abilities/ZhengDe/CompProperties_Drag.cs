using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompProperties_Drag : CompProperties_AbilityEffect
    { 
        
        public CompProperties_Drag()
        {
            compClass = typeof(CompAbilityEffect_Drag);
        }
    }
}