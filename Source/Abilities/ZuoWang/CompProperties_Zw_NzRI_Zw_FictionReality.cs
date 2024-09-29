using RimWorld;
using Verse;
using System;
using System.Linq;
using System.Collections.Generic;



namespace NzRimImmortalBizarre
{
    public class FictionReality : CompProperties_AbilityEffect {

        public List<ThingList> thingLists = new List<ThingList>();

        public FictionReality() {
            compClass = typeof(CompAbilityEffect_FictionReality);
        }
     }
}