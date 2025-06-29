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
            if (EnergyRootUtility.GetRoot(pawn).openingBasicAbility == false)
            {
                parent.Severity = 0;
            }
        }
        public override void CompPostMake()
        {
            if (EnergyRootUtility.GetRoot(pawn).openingBasicAbility == false)
            {
                EnergyRootUtility.GetRoot(pawn).openingBasicAbility = true;
                base.CompPostMake();
                return;
            }
            if (EnergyRootUtility.GetRoot(pawn).openingBasicAbility == true)
            {
                EnergyRootUtility.GetRoot(pawn).openingBasicAbility = false;
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