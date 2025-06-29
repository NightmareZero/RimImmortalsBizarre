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
        bool doGetLabel = true;
        public override string Label
        {
            get
            {
                if (this.Tracker == null || !doGetLabel)
                {
                    return "Label_FeiGang".Translate();
                }

                try
                {
                    var labelStr = "Label_FeiGang".Translate() + this.Tracker.getFgStr();
                    if (this.Tracker.getYqStr() != "")
                    {
                        labelStr += " " + "Label_InnateQi".Translate() + this.Tracker.getYqStr();
                    }
                    return labelStr;
                }
                catch (Exception e)
                {
                    Log.Error("Zw_Fg Label Error: " + e.Message);
                    doGetLabel = false;
                    return "Label_FeiGang".Translate();
                }

            }
        }


        public Zw_Tracker Tracker;
        private int tickInterval = 600;

        public override string LabelInBrackets => Tracker?.bar1Num?.ToString() ?? "";

        private Gizmo_EnergyBar energyBar = null;

        public override void PostMake()
        {
            base.PostMake();
            this.Init();
        }
        public override void Tick()
        {
            base.Tick();
            // 计算tick触发
            if (this.ageTicks % tickInterval == 0)
            {
                this.Tracker.Tick(ageTicks);
                this.energyBar?.ResetDrawCfg();
            }

        }

        public override void ExposeData()
        {
#if DEBUG
            Log.Message("Zw_Fg ExposeData");
#endif
            base.ExposeData();
            Scribe_Deep.Look(ref Tracker, "zwTracker");

            this.Tracker?.injectHediff(this);
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
                    icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
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
                    icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
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
                    icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
                    action = delegate
                    {
                        // 生成非罡
                        Tracker.mustChangeFg(-1000);
                    }
                };
                yield return new Command_Action
                {
                    defaultLabel = "来个心素",
                    defaultDesc = "心素",
                    icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
                    action = delegate
                    {
                        // 创建事件参数
                        IncidentParms parms = new IncidentParms();
                        parms.target = Find.CurrentMap; // 设置事件目标为当前地图
                        // 触发事件，游荡心素、
                        IncidentDef1Of.Nz_XinSuWandersIn.Worker.TryExecute(parms);

                    }
                };
                if (this.Tracker != null && this.Tracker.bar2Num.Enabled())
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "来点先天一气",
                        defaultDesc = "给我整点先天一气",
                        icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
                        action = delegate
                        {
                            // 生成非罡
                            Tracker.ChangeYq(30);
                        }
                    };
                    yield return new Command_Action
                    {
                        defaultLabel = "来一堆先天一气",
                        defaultDesc = "整满",
                        icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
                        action = delegate
                        {
                            // 生成非罡
                            Tracker.ChangeYq(1000);
                        }
                    };
                    yield return new Command_Action
                    {
                        defaultLabel = "先天一气清除",
                        defaultDesc = "清空",
                        icon = ContentFinder<Texture2D>.Get("UI/Abilities/Base/LG"),
                        action = delegate
                        {
                            // 生成非罡
                            Tracker.mustChangeYq(-1000);
                        }
                    };
                }
            }

            // 能量条
            if (this.Tracker != null)
            {
                if (energyBar == null)
                {
                    energyBar = new Gizmo_EnergyBar(Tracker);
                }
                if (this.Tracker.Visible)
                {
                    yield return energyBar;
                }
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

            this.Tracker?.injectHediff(this);
        }
    }
}