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

        private Hediff addHediff;
        private BodyPartRecord addBodyPart;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            // 预留资源
            if (!this.prepareLevelUp(this.pawn))
            {
                return false;
            }

            // 原版预留资源
            return pawn.Reserve(base.TargetLocA, job, 1, -1, null, errorOnFailed);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            // 设置目标
            this.FailOnDespawnedOrNull(TargetIndex.A);

            this.AddFinishAction((JobCondition condition) =>
            {
                if (condition == JobCondition.Succeeded)
                {
                    // 只有在动作没有被打断的情况下才执行
                    Pawn actor = this.pawn;
                    try
                    {
                        applyLevelUp(actor);
                    }
                    catch (Exception e)
                    {
                        e.PrintExceptionWithStackTrace();
                    }
                    Log.Message("Job completed successfully.");
                }
                else
                {
                    // TODO 走火入魔吧 添加仙路那个Hediff
                }

            });

            // 创建一个新的 Toil
            Toil doJob = new Toil();
            // 初始化动作
            doJob.initAction = () =>
            {
                Pawn actor = doJob.actor;
                // 你可以在这里添加初始化逻辑
                // TODO Message输出
            };
            doJob.tickAction = () =>
            {
                Pawn actor = doJob.actor;
                // 每个 tick 执行的动作
            };
            doJob.defaultCompleteMode = ToilCompleteMode.Delay;
            doJob.defaultDuration = JobDuration;
            // 添加进度条
            doJob.WithProgressBar(TargetIndex.A, () => (float)doJob.actor.jobs.curDriver.ticksLeftThisToil / JobDuration, false);
            // doJob.PlaySoundAtEnd
            // doJob.PlaySoundAtStart

            yield return doJob;
        }

        private bool prepareLevelUp(Pawn pawn)
        {
            Zd_Fruition fruition = Utils.GetFruitionHediff(pawn);
            if (fruition == null)
            {
                // TODO Message输出
                Log.Error("Fruition hediff not found.");
                return false;
            }

            DefZdCultivationLine lineDef = DataOf.DefCultivationLineDictByLine.TryGetValue(fruition.wayLevelUp);
            if (lineDef == null)
            {
                // TODO Message输出
                Log.Error($"Cultivation line {fruition.wayLevelUp} not found.");
#if DEBUG
                Log.Error($"WayLevelUp: {fruition.wayLevelUp} All: {string.Join(", ", DataOf.DefCultivationLineDict.Keys.ToList())}");
#endif
                return false;
            }

            bool got = ZdLevelUpUtil.PreCreateBodyPart(pawn, lineDef, out Hediff addedHediff, out BodyPartRecord bodyPart);
            if (!got)
            {
                // TODO Message输出
                Log.Error("Pre create body part failed.");
                return false;
            }

            this.addHediff = addedHediff;
            this.addBodyPart = bodyPart;

            return true;
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


            // 添加预创建好的Hediff
            pawn.health.AddHediff(this.addHediff, this.addBodyPart);
#if DEBUG
#else
            fruition.wayLevelUp = null;
            fruition.Tracker.resetAfterLevelUp();
#endif
            // TODO Message输出, 突破成功
        }


        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.addHediff, "addHediff");
            Scribe_Values.Look(ref this.addBodyPart, "addBodyPart");
        }

        /*
        1. GetReport：
           - 返回当前任务的报告字符串，通常用于显示在用户界面上。子类可以重写此方法以提供自定义的报告字符串。

        2. TryMakePreToilReservations：
           - 尝试为任务的目标预留资源。子类必须实现此方法，以确保任务所需的资源在任务开始前已被预留。

        3. MakeNewToils：
           - 创建并返回一个 Toil 的集合，这些 Toil 定义了任务的具体步骤。子类必须实现此方法，以定义任务的具体行为。

        4. SetInitialPosture：
           - 设置角色的初始姿势。默认情况下，角色站立。子类可以重写此方法以设置不同的初始姿势。

        5. ExposeData：
           - 用于保存和加载任务的状态。子类可以重写此方法以保存和加载自定义数据。

        6. CanBeginNowWhileLyingDown：
           - 指示任务是否可以在角色躺下时开始。默认返回 false。子类可以重写此方法以允许任务在角色躺下时开始。

        7. TaleParameters：
           - 返回一个对象数组，包含与任务相关的故事参数。子类可以重写此方法以提供自定义的故事参数。

        8. Notify_Starting：
           - 在任务开始时调用。子类可以重写此方法以在任务开始时执行自定义逻辑。

        9. Notify_PatherArrived：
           - 在角色到达路径目标时调用。子类可以重写此方法以在角色到达目标时执行自定义逻辑。

        10. Notify_PatherFailed：
            - 在路径查找失败时调用。子类可以重写此方法以在路径查找失败时执行自定义逻辑。

        11. Notify_StanceChanged：
            - 在角色姿势改变时调用。子类可以重写此方法以在角色姿势改变时执行自定义逻辑。

        12. Notify_DamageTaken：
            - 在角色受到伤害时调用。子类可以重写此方法以在角色受到伤害时执行自定义逻辑。

        13. ModifyCarriedThingDrawPos：
            - 修改角色携带物品的绘制位置。子类可以重写此方法以自定义物品的绘制位置。

        14. DesiredSocialMode：
            - 返回任务所需的社交模式。子类可以重写此方法以指定任务的社交模式。

        15. IsContinuation：
            - 指示给定的任务是否是当前任务的延续。子类可以重写此方法以自定义任务的延续逻辑。

        16. IsSameJobAs：
            - 指示给定的任务是否与当前任务相同。子类可以重写此方法以自定义任务的相同性逻辑。
        
        */
    }
}