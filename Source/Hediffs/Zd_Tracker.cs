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

        public const string Fruition = "fruition";
        public const string BuddhaNature = "buddhaNature";
        public const string SelfSacrifice = "selfSacrifice";

        private Pawn pawn;
        private Zd_Fruition parent;

        // 修行进度
        private float fruition = 0f;

        // 佛性
        private float buddhaNature = 0f;

        // 舍身
        private float selfSacrifice = 0f;

        /// <summary>
        /// 刷新修行进度(当完成一次进阶时)
        /// </summary>
        public void resetAtLevelUp()
        {
            if (!checkEnergyRoot())
            {
                return;
            }
            this.fruition = Mathf.Clamp(this.fruition - 99, 0, 200);
            this.buddhaNature = Mathf.Clamp(this.buddhaNature - 49, 0, 100);
            this.selfSacrifice = Mathf.Clamp(this.selfSacrifice - 49, 0, 100);
        }

        /// <summary>
        /// 可以进阶
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool canLevelUp(string type)
        {

            switch (type)
            {
                case Fruition:
                    return this.fruition >= 99;
                case BuddhaNature:
                    return this.buddhaNature >= 50;
                case SelfSacrifice:
                    return this.selfSacrifice >= 50;
                default:
                    return false;
            }
        }

        public void addFruition(float value)
        {
            if (!checkEnergyRoot())
            {
                return;
            }
            // 当修行进度达到100，修行速度减半
            if (this.fruition >= 100 && value > 0)
            {
                this.buddhaNature = Mathf.Clamp(this.buddhaNature + value / 2, 0, 200);
                return;
            }
            this.fruition = Mathf.Clamp(this.fruition + value, 0, 200);
        }

        /// <summary>
        /// 添加佛性
        /// </summary>
        /// <param name="value"></param>
        public void addBuddhaNature(float value = 10)
        {
            if (!checkEnergyRoot())
            {
                return;
            }
            this.buddhaNature = Mathf.Clamp(this.buddhaNature + value, 0, 200);
        }

        /// <summary>
        /// 添加舍身
        /// </summary>
        /// <param name="value"></param>
        public void addSelfSacrifice(float value = 10)
        {
            if (!checkEnergyRoot())
            {
                return;
            }
            this.selfSacrifice = Mathf.Clamp(this.selfSacrifice + value, 0, 200);
        }


        public void injectHediff(Zd_Fruition fgHediff)
        {
            if (!checkEnergyRoot())
            {
                return;
            }
            this.parent = fgHediff;
            this.pawn = fgHediff.pawn;
        }

        private bool checkEnergyRoot()
        {
            if (Utils.TryGetEnergyRoot(pawn) == null)
            {
                // 移除本 Hediff
                pawn.health.RemoveHediff(parent);
                Messages.Message("Zd_Cultivation_LostEnergyRoot".Translate(pawn.Name.Named("pawnName")), pawn, MessageTypeDefOf.NegativeEvent);
                return false;
            }
            return true;
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref fruition, "fruition");
            Scribe_Values.Look(ref buddhaNature, "buddhaNature");
            Scribe_Values.Look(ref selfSacrifice, "selfSacrifice");
        }

    }
}