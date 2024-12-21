using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    public class Zd_Fruition : Hediff
    {
        // 显示修行进度(形容词)和偏向
        //  public override string Label
        // {
        //     get
        //     {
        //         var doGet = true;
        //         if (this.Tracker == null || !doGet) { 
        //             return "Label_FeiGang".Translate();
        //         }

        //         try
        //         {
        //             var labelStr = "Label_FeiGang".Translate() + this.Tracker.getFgStr();
        //             if (this.Tracker.getYqStr() != "")
        //             {
        //                 labelStr += " " + "Label_InnateQi".Translate() + this.Tracker.getYqStr();
        //             }
        //             return  labelStr;
        //         }
        //         catch (Exception e)
        //         {
        //             Log.Error("Zw_Fg Label Error: " + e.Message);
        //             doGet = false;
        //             return "Label_FeiGang".Translate();
        //         }

        //     }
        // }

        // 显示修行进度(数值)
        // public override string Description 
        // { 

        // }

        
        public Zd_Tracker Tracker;
        public string wayLevelUp = "";
        public bool Removed { get; private set; } = false;

        public override void PostMake()
        {
            base.PostMake();
            this.Init();
        }

        public override void PostRemoved()
        {
            base.PostRemoved();
            this.Removed = true;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Deep.Look(ref Tracker, "zwTracker");

            this.Init();
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            // 上帝模式图标
            if (DebugSettings.godMode)
            {
                // 然后返回自定义的Gizmo
                yield return new Command_Action
                {
                    defaultLabel = "充满修行进度",
                    defaultDesc = "充满修行进度",
                    icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                    action = () =>
                    {
                        this.Tracker.addFruition(999);
                        this.Tracker.addSelfSacrifice(999);
                        this.Tracker.addBuddhaNature(999);
                        // this.Tracker.Reset();
                    }
                };
            }

            if (this.Tracker != null)
            {
                if (this.Tracker.canLevelUp(Zd_Tracker.Fruition))
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Zd_Fruition_LevelUp".Translate(),
                        defaultDesc = "Zd_Fruition_LevelUp_Desc".Translate(),
                        action = () =>
                        {
                            wayLevelUp = Zd_Tracker.Fruition;
                            // TODO 弹出确认提示
                            goToLevelUp();
                        }
                    };
                }
                if (this.Tracker.canLevelUp(Zd_Tracker.SelfSacrifice))
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Zd_Fruition_LevelUp_SelfSacrifice".Translate(),
                        defaultDesc = "Zd_Fruition_LevelUp_SelfSacrifice_Desc".Translate(),
                        action = () =>
                        {
                            wayLevelUp = Zd_Tracker.SelfSacrifice;
                            // TODO 弹出确认提示
                            goToLevelUp();
                        }
                    };
                }
                if (this.Tracker.canLevelUp(Zd_Tracker.BuddhaNature))
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Zd_Fruition_LevelUp_BuddhaNature".Translate(),
                        defaultDesc = "Zd_Fruition_LevelUp_BuddhaNature_Desc".Translate(),
                        action = () =>
                        {
                            wayLevelUp = Zd_Tracker.BuddhaNature;
                            // TODO 弹出确认提示
                            goToLevelUp();
                        }
                    };
                }
            }
        }

        private void goToLevelUp()
        {
            Job job = JobMaker.MakeJob(XmlOf.Job_Zd_LevelUp, pawn);
            pawn.jobs.StartJob(
                newJob: job,
                lastJobEndCondition: JobCondition.InterruptForced,
                jobGiver: null,
                resumeCurJobAfterwards: false,
                cancelBusyStances: true,
                thinkTree: null,
                tag: null,
                fromQueue: false,
                canReturnCurJobToPool: false,
                keepCarryingThingOverride: false,
                continueSleeping: false,
                addToJobsThisTick: true,
                preToilReservationsCanFail: false
            );
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
            this.Tracker.injectHediff(this);
        }
    }
}