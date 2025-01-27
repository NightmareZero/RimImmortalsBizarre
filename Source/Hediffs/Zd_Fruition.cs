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
        bool doGetLabel = true;
        // 显示修行进度(形容词)和偏向
        public override string Label
        {
            get
            {

                if (this.Tracker == null || !doGetLabel)
                {
                    return "Zd_Fruition_Label".Translate();
                }

                try
                {
                    var labelStr = "Zd_Fruition_Label".Translate();
                    var status = "Zd_Fruition_Label_Pre".Translate();
                    List<string> statusList = new List<string>();

                    if (this.Tracker.canLevelUp(Zd_Tracker.WayFruition))
                    {
                        statusList.Add("Zd_Fruition_Restraint".Translate());
                    }
                    if (this.Tracker.canLevelUp(Zd_Tracker.WaySacrifice))
                    {
                        statusList.Add("Zd_Fruition_SelfSacrifice".Translate());
                    }
                    if (this.Tracker.canLevelUp(Zd_Tracker.WayEnlightenment))
                    {
                        statusList.Add("Zd_Fruition_BuddhaNature".Translate());
                    }

                    if (statusList.Count > 0)
                    {
                        status = string.Join(", ", statusList);
                    }

                    return labelStr + " (" + status + ")";
                }
                catch (Exception e)
                {
                    Log.Error("Zd_Fruition Label Error: " + e.Message);
                    doGetLabel = false;
                    return "Zd_Fruition_Label".Translate();
                }

            }
        }

        bool doGetDesc = true;
        // 显示修行进度(数值)
        public override string Description
        {
            get
            {
                if (this.Tracker == null || !doGetDesc)
                {
                    return "Zd_Fruition_Desc".Translate();
                }

                try
                {
                    var descStr = "Zd_Fruition_Desc".Translate();

                    descStr += "\n" + "Zd_Fruition_Restraint".Translate() + ": " + this.Tracker.Fruition;
                    descStr += "\n" + "Zd_Fruition_SelfSacrifice".Translate() + ": " + this.Tracker.SelfSacrifice;
                    descStr += "\n" + "Zd_Fruition_BuddhaNature".Translate() + ": " + this.Tracker.BuddhaNature;
                    return descStr;
                }
                catch (Exception e)
                {
                    Log.Error("Zd_Fruition Desc Error: " + e.Message);
                    doGetDesc = false;
                    return "Zd_Fruition_Desc".Translate();
                }
            }
        }


        public Zd_Tracker Tracker;
        public string wayLevelUp = "";
        public bool Removed { get; private set; } = false;
        private bool needsInit = false;

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

            needsInit = true;
        }

        public override void Tick()
        {
            base.Tick();
            if (needsInit)
            {
                needsInit = false;
                this.Init();
            }
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
                if (this.Tracker.canLevelUp(Zd_Tracker.WayFruition))
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Zd_Fruition_LevelUp".Translate(),
                        defaultDesc = "Zd_Fruition_LevelUp_Desc".Translate(),
                        icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                        action = () =>
                        {
                            wayLevelUp = Zd_Tracker.WayFruition;
                            // TODO 弹出确认提示
                            goToLevelUp();
                        }
                    };
                }
                if (this.Tracker.canLevelUp(Zd_Tracker.WaySacrifice))
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Zd_Fruition_LevelUp_SelfSacrifice".Translate(),
                        defaultDesc = "Zd_Fruition_LevelUp_SelfSacrifice_Desc".Translate(),
                        icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                        
                        action = () =>
                        {
                            wayLevelUp = Zd_Tracker.WaySacrifice;
                            // TODO 弹出确认提示
                            goToLevelUp();
                        }
                    };
                }
                if (this.Tracker.canLevelUp(Zd_Tracker.WayEnlightenment))
                {
                    yield return new Command_Action
                    {
                        defaultLabel = "Zd_Fruition_LevelUp_BuddhaNature".Translate(),
                        defaultDesc = "Zd_Fruition_LevelUp_BuddhaNature_Desc".Translate(),
                        icon = ContentFinder<Texture2D>.Get("Ability/Base/LG"),
                        action = () =>
                        {
                            wayLevelUp = Zd_Tracker.WayEnlightenment;
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