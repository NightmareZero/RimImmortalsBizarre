using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static partial class Thought1Def {
        
        // 救了我
        public static ThoughtDef NzRI_SaveMe;

        // 骗了我
        public static ThoughtDef NzRI_CheatMe;

        // 说话动听
        public static ThoughtDef NzRI_T_GracefulWords;

        // 说话难听
        public static ThoughtDef NzRI_T_NonsenseWords;

        // 麻将牌
        public static HediffDef NzRI_Zw_Mahjong;

        // 讨论特效，亲切
        public static InteractionDef KindWords;

        // 讨论特效，笑脸
        public static InteractionDef WordOfJoy;

        // 讨论特效，愤怒
        public static InteractionDef Slight;

        // 讨论特效，侮辱
        public static InteractionDef Insult;

        public static void Init()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(Thought1Def));
        }
     }
}