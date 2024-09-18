using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;



namespace NzRimImmortalBizarre
{
    public class CompProperties_DropTargetItem : CompProperties_AbilityEffect { 

        public List<ThingCategoryDef> allowTypes = new List<ThingCategoryDef>() { 
            XmlOf.ApparelUtility,
            XmlOf.Weapons,
            XmlOf.Apparel,
        };

        public CompProperties_DropTargetItem()
        {
            compClass = typeof(CompAbilityEffect_DropTargetItem);
        }
    }
}