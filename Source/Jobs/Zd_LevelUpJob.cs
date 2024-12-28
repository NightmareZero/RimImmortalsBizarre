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
    public class JobDriver_Zd_LevelUp : JobDriver
    {
        private const int JobDuration = 400; // 400 ticks = 6.67 seconds

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            // 设置目标
            this.FailOnDespawnedOrNull(TargetIndex.A);

            // 创建一个新的 Toil
            Toil doJob = new Toil();
            doJob.initAction = () =>
            {
                // 初始化动作
                Pawn actor = doJob.actor;
                // 你可以在这里添加初始化逻辑
            };
            doJob.tickAction = () =>
            {
                // 每个 tick 执行的动作
                Pawn actor = doJob.actor;
                // 你可以在这里添加每个 tick 的逻辑
            };
            doJob.defaultCompleteMode = ToilCompleteMode.Delay;
            doJob.defaultDuration = JobDuration;
            doJob.AddFinishAction(() =>
            {
                // 动作完成后执行的逻辑
                if (!this.pawn.CurJob.playerForced)
                {
                    // 只有在动作没有被打断的情况下才执行
                    Pawn actor = doJob.actor;
                    applyLevelUp(actor);
                    Log.Message("Job completed successfully.");
                }
            });

            yield return doJob;
        }

        private void applyLevelUp(Pawn pawn)
        {
            Zd_Fruition fruition = Utils.GetFruitionHediff(pawn);
            if (fruition == null)
            {
                // TODO Message输出
                Log.Error("Fruition hediff not found.");
                return;
            }

            DefZdCultivationLine lineDef = DataOf.DefCultivationLineDict[fruition.wayLevelUp];
            if (lineDef == null)
            {
                // TODO Message输出
                Log.Error("Cultivation line not found.");
                #if DEBUG
                Log.Error($"WayLevelUp: {fruition.wayLevelUp} All: {string.Join(", ", DataOf.DefCultivationLineDict.Keys.ToList())}");
                #endif
                return;
            }

            bool ok = ZdLevelUpUtil.LevelUpAndAddBodyPart(pawn, lineDef,out Hediff addedHediff, out BodyPartRecord bodyPart);
            if (!ok)
            {
                // TODO Message输出
                Log.Error("Level up failed.");
                return;
            }
        }
    }
}