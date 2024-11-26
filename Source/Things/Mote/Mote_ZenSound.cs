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

        private void invoke() { 
            if (this.isFinished)
            {
                return;
            }

            // TODO
        }


        private bool isFinished = true;
        public override void SpawnSetup(Map map, bool respawningAfterLoad)
        {
            base.SpawnSetup(map, respawningAfterLoad);

            // 初始化后续逻辑
            this.isFinished = false; // 标记为未完成
        }

        public override void Tick()
        {
            base.Tick();

            if (this.isFinished)
            {
                return;
            }

            // 在第1.5秒执行效果
            if (this.AgeSecs >= 1.5f)
            {
                // // 播放音效
                // SoundDefOf.ZenSound.PlayOneShot(new TargetInfo(this.Position, this.Map, false));
                try
                {
                    this.invoke();
                }
                catch (System.Exception e)
                {
                    e.PrintExceptionWithStackTrace();
                } finally {
                    // 标记为已完成
                    this.isFinished = true;
                    this.Destroy();
                }
            }

        }
    }
}