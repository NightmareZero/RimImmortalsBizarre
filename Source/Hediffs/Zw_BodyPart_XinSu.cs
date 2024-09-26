using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    public class Zw_BodyPart_XinSuBase : Hediff_AddedPart, IXinSuPart
    {
        public override void Tick()
        {
            base.Tick();

            // 每隔1小时检查一次
            if (ageTicks % 2500 == 0)
            {
                if (MathUtil.IsProbability(1, 15000))
                {
                    // 让宿主触发崩溃，迷茫徘徊
                    pawn.mindState.mentalStateHandler
                    .TryStartMentalState(MentalStateDefOf.Wander_OwnRoom, reason: this.Label.Translate());


                }
            }
        }

    }

    public interface IXinSuPart
    {

    }
}
