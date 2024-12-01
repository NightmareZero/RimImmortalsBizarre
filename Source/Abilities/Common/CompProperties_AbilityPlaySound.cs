using RimWorld;
using System;
using System.Linq;
using Verse;
using Verse.Sound;



namespace NzRimImmortalBizarre
{ 
    public class CompProperties_AbilityPlaySound : CompProperties_AbilityEffect
{
    public SoundDef soundDef;

    public CompProperties_AbilityPlaySound()
    {
        this.compClass = typeof(CompAbilityEffect_PlaySound);
    }
}

public class CompAbilityEffect_PlaySound : CompAbilityEffect
{
    public new CompProperties_AbilityPlaySound Props => (CompProperties_AbilityPlaySound)this.props;

    public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
    {
        base.Apply(target, dest);

        if (Props.soundDef != null)
        {
            Props.soundDef.PlayOneShot(new TargetInfo(target.Cell, this.parent.pawn.Map, false));
        }
    }
}
}