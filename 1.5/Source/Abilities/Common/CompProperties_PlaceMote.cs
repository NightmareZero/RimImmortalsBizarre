using RimWorld;
using System;
using System.Linq;
using Verse;



namespace NzRimImmortalBizarre
{
    public class CompProperties_PlaceMote : CompProperties_AbilityEffect
    {

        public ThingDef moteDef;

        public CompProperties_PlaceMote()
        {
            compClass = typeof(CompAbilityEffect_PlaceMote);
        }
    }

    public class CompAbilityEffect_PlaceMote : CompAbilityEffect
    {
        public new CompProperties_PlaceMote Props => (CompProperties_PlaceMote)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            if (target.Cell.IsValid)
            {
                MoteMaker.MakeStaticMote(target.Cell, parent.pawn.Map, Props.moteDef);
            }
        }
    }
}