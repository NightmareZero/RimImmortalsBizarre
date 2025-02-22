using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using RimWorld;
using Unity.Mathematics;
using UnityEngine;
using Verse;
using Verse.AI;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    /// <summary>
    /// 为角色添加一个设定的精神崩溃
    /// </summary>
    /// 

    public class Comp_MentalBreakGiver : HediffCompProperties
    {
        // 精神崩溃类型
        public MentalStateDef mentalBreak;
        //  MentalStateDefOf

        // 呕吐
        public bool vomit = false;


        // 立即的
        public bool immediate = true;

        // tick间隔(0为不发生)
        public int tickInterval = 0;
        // 严重度概率乘数(1为 100%)
        public float severityMultiplier = 1f;

        public Comp_MentalBreakGiver()
        {
            compClass = typeof(HediffComp_MentalBreakGiver);
        }
    }

    public class HediffComp_MentalBreakGiver : HediffComp
    {
        public Comp_MentalBreakGiver Props => (Comp_MentalBreakGiver)props;

        public Pawn pawn => parent.pawn;

        public override void CompPostPostAdd(DamageInfo? dinfo)
        {
            base.CompPostPostAdd(dinfo);

            if (Props.immediate)
            {
                TryGiveMentalBreak();
            }
        }

        public override void CompPostPostRemoved()
        {
            base.CompPostPostRemoved();

            // 移除时清除精神崩溃
            var state = pawn?.mindState?.mentalStateHandler?.CurState;
            if (state != null && state.def == Props.mentalBreak)
            {
                pawn.MentalState.RecoverFromState();
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);

            if (Props.tickInterval > 0 && parent.ageTicks % Props.tickInterval == 0)
            {
                if (Props.severityMultiplier > 0)
                {
                    // 有概率造成精神崩溃
                    var percent = parent.Severity * Props.severityMultiplier;
                    if (Rand.Chance(percent))
                    {
                        TryGiveMentalBreak();
                    }
                }
            }
        }

        private void TryGiveMentalBreak()
        {
            if (pawn?.mindState?.mentalStateHandler?.CurState == null)
            {

                if (Props.vomit)
                {
                    pawn.jobs.StartJob(new Job(JobDefOf.Vomit), JobCondition.InterruptForced);
                }
                var mentalBreakDef = Props.mentalBreak;
                if (mentalBreakDef != null)
                {
                    pawn.mindState.mentalStateHandler.TryStartMentalState(mentalBreakDef, null, false, false, false);
                    return;
                }

            }
        }
    }

}