using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using RimWorld;
using UnityEngine;
using Verse;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{ 
    public class Comp_SwitchDoCultivation : HediffComp
    {
        public Pawn pawn => base.Pawn;
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (pawn.GetRIRoot().openingBasicAbility == false)
            {
                parent.Severity = 0;
            }
        }
        public override void CompPostMake()
        {
            if (pawn.GetRIRoot().openingBasicAbility == false)
            {
                pawn.GetRIRoot().openingBasicAbility = true;
                base.CompPostMake();
                return;
            }
            if (pawn.GetRIRoot().openingBasicAbility == true)
            {
                pawn.GetRIRoot().openingBasicAbility = false;
                base.CompPostMake();
                return;
            }
            base.CompPostMake();
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
        }

    }
}