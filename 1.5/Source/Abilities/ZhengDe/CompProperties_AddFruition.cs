using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class CompProperties_AddFruition : CompProperties_AbilityEffect
    {
        public bool buddha = false;

        public bool sacrifice = false;
        public CompProperties_AddFruition()
        {
            compClass = typeof(CompAbilityEffect_AddFruition);
        }
    }
}