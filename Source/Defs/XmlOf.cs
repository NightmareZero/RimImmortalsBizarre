using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    [DefOf]
    public static class XmlOf
    {
        // public static DefPartHurtLevel defPartHurt;

        public static AbilityDef NzRI_AoJing_FingerNail;

        public static SoundDef NzRI_HurtSelf;

        public static HediffDef Burn;



        static XmlOf()
        {
            // Log.Message("Initializing XmlOf...");
            DefOfHelper.EnsureInitializedInCtor(typeof(XmlOf));

            DataOf.Init();

            // 自动查找并初始化所有DefPartHurtLevel类型

            // Log.Message("XmlOf initialized.");
        }
    }

    public static class DataOf
    {
        // 定义一个包含所有DefPartHurtLevel类型的数组
        public static List<DefPartHurtLevel> allDefPartHurts => DefDatabase<DefPartHurtLevel>.AllDefsListForReading;
        public static Dictionary<string, DefPartHurtLevel> allDefPartHurtsDict = new Dictionary<string, DefPartHurtLevel>();

        // 定义一个包含所有伤害类型的数组
        public static List<DamageDef> allDamageDefs => DefDatabase<DamageDef>.AllDefsListForReading;
        public static Dictionary<string, DamageDef> allDamageDefsDict = new Dictionary<string, DamageDef>();

        public static void Init() { }

        static DataOf()
        {

            #region Debug
            Log.Message("NzRimImmortalBizarre: got DefPartHurtLevel: " + allDefPartHurts.Count);
            #endregion

            // 所有DefPartHurtLevel类型
            foreach (var def in allDefPartHurts)
            {
                allDefPartHurtsDict.Add(def.defName, def);
            }

            // 所有伤害类型
            foreach (var def in allDamageDefs)
            {
                allDamageDefsDict.Add(def.defName, def);
            }



        }
    }

}