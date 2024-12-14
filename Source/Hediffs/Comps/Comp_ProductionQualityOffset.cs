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
    public class Comp_ProductionQualityOffset : HediffComp
    { 
        // 质量偏移
        public int offset = 0;

        // TODO Harmony Patch QualityUtility.GenerateQualityCreatedByPawn
    }
}