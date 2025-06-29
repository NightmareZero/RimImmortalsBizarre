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
    public static class Caches
    {
        public static Dictionary<Pawn, HediffComp_ProductionQualityOffset> productionQualityOffsetCache = new Dictionary<Pawn, HediffComp_ProductionQualityOffset>();


        /// <summary>
        /// 载入存档时, 清空缓存
        /// </summary>
        public static void Clear()
        {
            productionQualityOffsetCache.Clear();
        }
    }
}