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

        public static SoundDef NzRI_FeedMeat;

        public static HediffDef Burn;

        public static SoundDef Explosion_GiantBomb;

        public static EffecterDef GiantExplosion;

        // 登阶
        public static HediffDef NzRI_AoJing_Ascend;

        // 剧痛
        public static HediffDef NzRI_AoJing_Agony;

        // 非罡
        public static HediffDef NzRI_Zw_Fg;

        // 果位
        public static HediffDef NzRI_Zd_Fruition;

        // 正德寺 突破Job
        public static JobDef Job_Zd_LevelUp;



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