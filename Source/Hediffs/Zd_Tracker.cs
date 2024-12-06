using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;
using WhoXiuXian;

namespace NzRimImmortalBizarre
{
    public class Zd_Tracker : IExposable
    {

        private Pawn pawn;

        // 修行进度
        private float fruition = 0f;

        // 佛性
        private float buddhaNature = 0f;

        // 舍身
        private float selfSacrifice = 0f;

        /// <summary>
        /// 刷新修行进度(当完成一次进阶时)
        /// </summary>
        public void resetFruition()
        {
            this.fruition = Mathf.Clamp(this.fruition - 99, 0, 200);
            this.buddhaNature = Mathf.Clamp(this.buddhaNature - 49, 0, 100);
            this.selfSacrifice = Mathf.Clamp(this.selfSacrifice - 49, 0, 100);
        }

        public void addFruition(float value)
        {
            // 当修行进度达到100，修行速度减半
            if (this.fruition >= 100 && value > 0)
            {
                this.buddhaNature = Mathf.Clamp(this.buddhaNature + value / 2, 0, 200);
                return;
            }
            this.fruition = Mathf.Clamp(this.fruition + value, 0, 200);
        }

        public void addBuddhaNature(float value)
        {
            this.buddhaNature = Mathf.Clamp(this.buddhaNature + value, 0, 200);
        }

        public void addSelfSacrifice(float value)
        {
            this.selfSacrifice = Mathf.Clamp(this.selfSacrifice + value, 0, 200);
        }

        public void injectHediff(Zd_Fruition fgHediff)
        {
            this.pawn = fgHediff.pawn;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref fruition, "fruition");
            Scribe_Values.Look(ref buddhaNature, "buddhaNature");
            Scribe_Values.Look(ref selfSacrifice, "selfSacrifice");
        }

    }
}