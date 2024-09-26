using System.Text.RegularExpressions;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Noise;
using System.Collections.Generic;
using System;

namespace NzRimImmortalBizarre
{
    public static class MathUtil { 
        private static Random random = new Random();

        /// <summary>
        /// 有一定概率返回true
        /// </summary>
        /// <param name="numerator">分子</param>
        /// <param name="denominator">分母</param>
        /// <returns></returns>
        public static bool IsProbability(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                return false;
            }

            double probability = (double)numerator / denominator;
            return random.NextDouble() <= probability;
        }
    }
}