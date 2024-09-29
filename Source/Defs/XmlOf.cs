using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static partial class XmlOf
    {
        // public static DefPartHurtLevel defPartHurt;

        public static AbilityDef NzRI_AoJing_FingerNail;

        public static SoundDef NzRI_HurtSelf;

        public static HediffDef Burn;

        public static SoundDef Explosion_GiantBomb;

        public static EffecterDef GiantExplosion;

        // 登阶
        public static HediffDef NzRI_AoJing_Ascend;

        // 剧痛
        public static HediffDef NzRI_AoJing_Agony;

        // 非罡
        public static HediffDef NzRI_Zw_Fg;

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


        static XmlOf()
        {
            // Log.Message("Initializing XmlOf...");
            DefOfHelper.EnsureInitializedInCtor(typeof(XmlOf));

            DataOf.Init();

            // 自动查找并初始化所有DefPartHurtLevel类型

            // Log.Message("XmlOf initialized.");
        }
    }


}