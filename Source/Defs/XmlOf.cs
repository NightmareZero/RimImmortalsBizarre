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
        public static List<DefPartHurtLevel> allDefPartHurts;

        public static void Init() { }

        static DataOf()
        {

            // 自动查找并初始化所有DefPartHurtLevel类型
            allDefPartHurts = DefDatabase<DefPartHurtLevel>.AllDefsListForReading;
            #region Debug
            Log.Message("NzRimImmortalBizarre: got DefPartHurtLevel: " + allDefPartHurts.Count);
            #endregion

        }
    }

}