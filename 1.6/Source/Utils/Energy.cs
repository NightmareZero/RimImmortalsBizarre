using Verse;
using RimWorld;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    {
        /// <summary>
        /// 增加或者减少Pawn的非罡值
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="value">要添加的值</param>
        /// <returns>是否有足够的非罡(通常是消耗的时候)</returns>
        public static bool ChangeFeiGang(this Pawn pawn, float value)
        {
            Zw_Fg fg = AssertGetFeiGangHediff(pawn);
#if DEBUG
            Log.Message("ChangeFeiGang: " + value);
#endif

            return fg.Tracker.ChangeFg(value);
        }


        /// <summary>
        /// 增加或者减少Pawn的先天一气
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool ChangeYiQi(this Pawn pawn, float value)
        {
            Zw_Fg fg = AssertGetFeiGangHediff(pawn);
#if DEBUG
            Log.Message("ChangeYiQi: " + value);
#endif            
            return fg.Tracker.ChangeYq(value);
        }

        /// <summary>
        /// 是否有足够的非罡值
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="value">非罡值，传入正负数都会当作消耗</param>
        /// <returns></returns>
        public static bool HasEnoughFeiGang(this Pawn pawn, float value)
        {
            if (value >= 0)
            {
                return true;
            }

            Zw_Fg fg = GetFeiGangHediff(pawn);
            if (fg == null)
            {
                return false;
            }

            return fg.Tracker.EnoughFg(value);
        }

        /// <summary>
        /// 是否有足够的先天一气
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasEnoughYiQi(this Pawn pawn, float value)
        {
            if (value >= 0)
            {
                return true;
            }

            Zw_Fg fg = GetFeiGangHediff(pawn);
            if (fg == null)
            {
                return false;
            }

            return fg.Tracker.EnoughYq(value);
        }
    }
}