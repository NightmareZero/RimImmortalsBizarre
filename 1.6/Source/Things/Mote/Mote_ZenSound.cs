using System.Collections.Generic;
using UnityEngine;
using Verse;
using RimWorld;
using NzRimImmortalBizarre;
using System.Linq;

namespace NzRimImmortalBizarre
{
    public class Mote_ZenSound : Mote
    {
        // MoteThrown
        // MoteAttached
        private int range { get; set; } = 5;

        EffecterDef effecterDef = DefDatabase<EffecterDef>.GetNamed("NzRI_Ef_ZenSound2");

        private Effecter effecter;

        private void invoke()
        {
            if (this.isFinished)
            {
                return;
            }

            // 获取Mote所在的Cell
            Utils.ApplyPawnInAffectedArea(this.Map, this.Position, this.range, invokeEveryPawn);
            // SoundDefOf.ZenSound.PlayOneShot(new TargetInfo(pawn.Position, pawn.Map, false));
        }

        private void invokeEveryPawn(Pawn pawn)
        {
#if DEBUG
            Log.Message("Mote_ZenSound.invokeEveryPawn: " + pawn.Name);
#endif
            // 解除Pawn的所有MentalState
            if (pawn.InMentalState)
            {
                pawn.MentalState.RecoverFromState();
            }

            // 给Pawn添加一个Hediff
            Hediff hediff = HediffMaker.MakeHediff(Thought1Def.NzRI_Zd_ZenSound, pawn);
            pawn.health.AddHediff(hediff);
        }


        private bool isFinished = true;
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);
#if DEBUG
            Log.Message("Mote_ZenSound.SpawnSetup");
#endif            

            // 初始化后续逻辑
            this.isFinished = false; // 标记为未完成
        }

        protected override void Tick()
        {
            base.Tick();

            if (this.isFinished)
            {
                return;
            }
            if (this.effecter != null)
            {
                this.effecter.EffectTick(new TargetInfo(this.Position, this.Map), new TargetInfo(this.Position, this.Map));
            }
            if (this.AgeSecs >= 0.3f && this.effecter == null)
            {
                Log.Message("Mote_ZenSound.Tick: " + this.AgeSecs);
                effecter = effecterDef.Spawn();
                effecter.Trigger(new TargetInfo(this.Position, this.Map), new TargetInfo(this.Position, this.Map));
                // effecter.Trigger(this.Position, target);
            }

            // 在第1.5秒执行效果
            if (this.AgeSecs >= 1.5f)
            {
                // // 播放音效
                // SoundDefOf1.ZenSound.PlayOneShot(new TargetInfo(this.Position, this.Map, false));
                try
                {
                    this.invoke();
                }
                catch (System.Exception e)
                {
                    e.PrintExceptionWithStackTrace();
                }
                finally
                {
                    // 标记为已完成
                    this.isFinished = true;
                    this.Destroy();
                }
            }

        }

        public override void DeSpawn(DestroyMode mode = DestroyMode.Vanish)
        {
            base.DeSpawn(mode);

            this.effecter.Cleanup();

        }

    }
}