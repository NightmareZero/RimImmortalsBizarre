using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    // Core.Hediff_RI_EnergyRoot
    public class Zw_Fg : HediffWithComps {

        private int tickAge = 0;
        private int tickInterval = 300;


        public override void Tick()
        {
            // 计算tick触发
            if (tickAge++ == tickInterval) return; // 每tickInterval触发一次
            tickAge = 0;



            // var energyRoot = pawn?.health?.hediffSet?.GetFirstHediff<Hediff_RI_EnergyRoot>();
            // if (energyRoot == null)
            // {
            //     Log.Error("NzRI_ReduceFg_EnergyRootNotExist" + pawn.Name);
            //     return;
            // }

            // energyRoot.energy.ChangeEnergy(-1);
        }
    }
}