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
    public class Zd_Cultivation : Hediff
    {
        private Zd_Fruition _fruitionCache; // 不保存
        public override void Tick()
        {
            base.Tick();
            // 每隔1小时检查一次
            if (ageTicks % 2500 != 0)
            {
                return;
            }
            // 初始化 果位 Hediff
            if (_fruitionCache == null) { 
                _fruitionCache = Utils.GetFruitionHediff(this.pawn);
                if (_fruitionCache == null)
                { 
                    // 如果没有Hediff_Fruition，就添加一个
                    Hediff hediff = HediffMaker.MakeHediff(XmlOf.NzRI_Zd_Fruition, this.pawn);
                    this.pawn.health.AddHediff(hediff);
                    _fruitionCache = hediff as Zd_Fruition;
                }
            }
            // TODO 对果位进行处理
          
        }
    }
}