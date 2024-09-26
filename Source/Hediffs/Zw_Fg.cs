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
    // Core.Hediff_RI_EnergyRoot
    public class Zw_Fg : HediffWithComps
    {

        public Zw_Tracker Tracker;
        private int tickInterval = 600;

        public override string LabelInBrackets => Tracker?.bar1Num?.ToString() ?? "";

        private Gizmo_EnergyBar energyBar = null;

        public override void PostMake()
        {
            base.PostMake();
            this.Init();
            this.Tracker?.injectHediff(this);
        }
        public override void Tick()
        {
            base.Tick();
            // 计算tick触发
            if (this.ageTicks % tickInterval == 0)
            {
                this.Tracker.Tick();   
            }

            // TODO do something

            // var energyRoot = pawn?.health?.hediffSet?.GetFirstHediff<Hediff_RI_EnergyRoot>();
            // if (energyRoot == null)
            // {
            //     Log.Error("NzRI_ReduceFg_EnergyRootNotExist" + pawn.Name);
            //     return;
            // }

            // energyRoot.energy.ChangeEnergy(-1);
        }

        public override void ExposeData()
        {
#if DEBUG
            Log.Message("Zw_Fg ExposeData");
#endif
            base.ExposeData();
            Scribe_Deep.Look(ref Tracker, "zwTracker");
        }

        // 绘制界面
        public override IEnumerable<Gizmo> GetGizmos()
        {
            // 上帝模式图标
            if (DebugSettings.godMode)
            {
                // 然后返回自定义的Gizmo

                yield return new Command_Action
                {
                    defaultLabel = "来点非罡",
                    defaultDesc = "给我整点非罡",
                    icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                    action = delegate
                    {
                        // 生成非罡
                        Tracker.ChangeFg(10);
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "来一堆非罡",
                    defaultDesc = "整满",
                    icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                    action = delegate
                    {
                        // 生成非罡
                        Tracker.ChangeFg(1000);
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "非罡清除",
                    defaultDesc = "清空",
                    icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                    action = delegate
                    {
                        // 生成非罡
                        Tracker.mustChangeFg(-1000);
                    }
                };
            }

            // 能量条
            if (this.Tracker != null)
            {
                if (energyBar == null)
                {
                    energyBar = new Gizmo_EnergyBar(Tracker);
                }
                yield return energyBar;
            }

            // 返回基类的Gizmos
            foreach (var gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
        }

        // 被移除时
        public override void PostRemoved()
        {
            base.PostRemoved();
        }

        private void Init()
        {
#if DEBUG
            Log.Message("Zw_Fg Init");
#endif
            if (Tracker == null)
            {
                Tracker = new Zw_Tracker();
            }
        }
    }
}