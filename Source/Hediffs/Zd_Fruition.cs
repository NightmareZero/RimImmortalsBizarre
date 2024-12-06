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
    public class Zd_Fruition : Hediff
    {
        public Zd_Tracker Tracker;


        public override void PostMake()
        {
            base.PostMake();
            this.Init();
        }

        public override void Tick()
        {
            base.Tick();
            // 计算tick触发
            // if (this.ageTicks % tickInterval == 0)
            // {
            //     this.Tracker.Tick(ageTicks);
            //     this.energyBar?.ResetDrawCfg();
            // }

        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look(ref Tracker, "zwTracker");

            this.Tracker?.injectHediff(this);
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            // 上帝模式图标
            if (DebugSettings.godMode)
            {
                // 然后返回自定义的Gizmo
                yield return new Command_Action
                {
                    defaultLabel = "Test",
                    defaultDesc = "Test",
                    action = () =>
                    {
                        // this.Tracker.Reset();
                    }
                };
            }
        }

        public void Init()
        {
#if DEBUG
            Log.Message("Zd_Fruition Init");
#endif
            if (this.Tracker == null)
            {
                this.Tracker = new Zd_Tracker();
            }
        }
    }
}