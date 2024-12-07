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
        const float trueTickCost = 5f;

        private bool _isInitialized = false;
        private Zd_Fruition _fruitionCache; // 不保存
        private Hediff_RI_EnergyRoot _rootEnergyCache; // 不保存
        public override void Tick()
        {
            onceInit();
            base.Tick();
            // 每隔20秒检查一次
            if (ageTicks % 1200 != 0)
            {
                return;
            }

            if (_rootEnergyCache == null)
            {
                // 移除本 Hediff
                pawn.health.RemoveHediff(this);
                Messages.Message("Zd_Cultivation_LostEnergyRoot".Translate(pawn.Name.Named("pawnName")), pawn, MessageTypeDefOf.NegativeEvent);
                return;
            }

            // 检查是否有足够的灵气
            if (_rootEnergyCache.energy.Energy < trueTickCost)
            {
                // 移除本 Hediff
                pawn.health.RemoveHediff(this);
                Messages.Message("Zd_Cultivation_NotEnoughEnergy".Translate(pawn.Name.Named("pawnName")), pawn, MessageTypeDefOf.NegativeEvent);
                return;
            }

            // 对果位进行处理(增加修行进度)
            _fruitionCache.Tracker.addFruition(1);

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
            // 初始化 灵根 Hediff
            if (_rootEnergyCache == null)
            {
                _rootEnergyCache = Utils.TryGetEnergyRoot(pawn);
            }
        }
    }
}