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
            Zw_Fg fg = GetFeiGangHediff(pawn);
            #if DEBUG
            Log.Message("ChangeFeiGang: " + value);
            #endif
            if (fg == null)
            {
                #if DEBUG
                Log.Warning("Pawn " + pawn.Name + " has no FeiGang hediff, creating one.");
                #endif
                // 生成一个非罡状态
                fg = HediffMaker.MakeHediff(XmlOf.NzRI_Zw_Fg, pawn) as Zw_Fg;
                if (value < 0) // 如果是消耗，此时已经判断消耗失败了
                {
                    return false;
                }
            }

            

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
            Zw_Fg fg = GetFeiGangHediff(pawn);
            if (fg == null)
            {
                return false;
            }

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
            Zw_Fg fg = GetFeiGangHediff(pawn);
            if (fg == null)
            {
                return true;
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
            Zw_Fg fg = GetFeiGangHediff(pawn);
            if (fg == null)
            {
                return true;
            }

            return fg.Tracker.EnoughYq(value);
        }
    }
}