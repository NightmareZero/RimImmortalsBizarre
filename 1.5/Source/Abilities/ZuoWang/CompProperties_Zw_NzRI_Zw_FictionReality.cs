using RimWorld;
using Verse;
using System;
using System.Linq;
using System.Collections.Generic;



namespace NzRimImmortalBizarre
{
    public class FictionReality : CompProperties_AbilityEffect {

        public List<ItemList> itemLists = new List<ItemList>();

        public FictionReality() {
            compClass = typeof(CompAbilityEffect_FictionReality);
        }
     }
}