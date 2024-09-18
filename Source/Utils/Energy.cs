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
            Zw_Fg fg = GetFeiGang(pawn);
            if (fg == null)
            {
                // 生成一个非罡状态
                fg = HediffMaker.MakeHediff(XmlOf.NzRI_Zw_Fg, pawn) as Zw_Fg;
                if (value < 0) // 如果是消耗，此时已经判断消耗失败了
                {
                    return false;
                }
            }

            fg.Tracker.ChangeFg(value);

            return false;
        }

        /// <summary>
        /// 是否有足够的非罡值
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="value">非罡值，传入正负数都会当作消耗</param>
        /// <returns></returns>
        public static bool HasEnoughFeiGang(this Pawn pawn, float value)
        {
            Zw_Fg fg = GetFeiGang(pawn);
            if (fg == null)
            {
                return false;
            }

            return fg.Tracker.EnoughFg(value);
        }
    }
}