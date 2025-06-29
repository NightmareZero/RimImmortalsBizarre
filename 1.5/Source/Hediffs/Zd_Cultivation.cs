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
    /// <summary>
    /// 修行中 Hediff
    /// </summary>
    public class Zd_Cultivation : HediffWithComps
    {
        // 每次修行消耗的灵气
        const float trueTickCost = 5f;
        // 每隔30秒增加一次修行进度
        const int cultivationSecTick = 30;

        private bool _isInitialized = false;
        private Zd_Fruition _fruitionCache; // 不保存
        public override void Tick()
        {
            onceInit();
            base.Tick();
            if (ageTicks % (cultivationSecTick * 60) != 0)
            {
                return;
            }

            // 检查是否已经被移除
            if (_fruitionCache.Removed)
            {
                pawn.health.RemoveHediff(this);
                return;
            }

            var eRoot = pawn.GetRIRoot();
            if (eRoot == null)
            {
                // 移除本 Hediff
                pawn.health.RemoveHediff(this);
                Messages.Message("Zd_Cultivation_LostEnergyRoot".Translate(pawn.Name.Named("pawnName")), pawn, MessageTypeDefOf.NegativeEvent);
                return;
            }

            // 检查是否有足够的灵气
            if (eRoot.energy.Energy < trueTickCost)
            {
                // 移除本 Hediff
                pawn.health.RemoveHediff(this);
                Messages.Message("Zd_Cultivation_NotEnoughEnergy".Translate(pawn.Name.Named("pawnName")), pawn, MessageTypeDefOf.NeutralEvent);
                return;
            }

            // 对果位进行处理(增加修行进度)
            _fruitionCache.Tracker.addFruition(1);
            eRoot.energy.SetEnergy(-trueTickCost);

        }

        private void onceInit()
        {
            if (_isInitialized)
            {
                return;
            }
            _isInitialized = true;

            // 初始化 果位 Hediff
            if (_fruitionCache == null)
            {
                _fruitionCache = Utils.AssertGetFruitionHediff(pawn);
            }
        }
    }
}