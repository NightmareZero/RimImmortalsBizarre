using RimWorld;
using Verse;
using System.Linq;
using System.Collections.Generic;
using System;

namespace NzRimImmortalBizarre
{ 
    
    public static class DataOf
    {
        // 所有损伤清单 DefPartHurtLevel
        public static List<DefPartHurtLevel> allDefPartHurts;
        public static Dictionary<string, DefPartHurtLevel> allDefPartHurtsDict = new Dictionary<string, DefPartHurtLevel>();

        // 所有的伤害类型 DamageDef
        public static List<DamageDef> allDamageDefs;
        public static Dictionary<string, DamageDef> allDamageDefsDict = new Dictionary<string, DamageDef>();

        // 所有的物品清单 ThingList
        public static List<ItemList> allItemLists;
        public static Dictionary<string, List<ThingDef>> thingListCache = new Dictionary<string, List<ThingDef>>();

        // 正德寺修行路线
        public static List<DefZdCultivationLine> DefCultivationLines;
        // 正德寺修行路线 DefName=> DefZdCultivationLine
        public static Dictionary<string, DefZdCultivationLine> DefCultivationLineDict = new Dictionary<string, DefZdCultivationLine>();
        // 正德寺修行路线 LineName=> DefZdCultivationLine
        public static Dictionary<string, DefZdCultivationLine> DefCultivationLineDictByLine = new Dictionary<string, DefZdCultivationLine>();

        public static List<ThingDef> GetCachedItemThingList(List<ItemList> thingLists)
        {
            List<ThingDef> result = new List<ThingDef>();
            thingLists.Sort((a, b) => a.defName.CompareTo(b.defName)); // 先排序

            // 生成缓存键
            string cacheKey = string.Join(",", thingLists.Select(tl => tl.defName).ToArray());
            if (thingListCache.ContainsKey(cacheKey))
            {
                return thingListCache[cacheKey];
            }

            foreach (var thingList in thingLists)
            {
                result.AddRange(thingList.things);
            }
            return result;
        }

        public static List<ThingDef> GetCachedItemThingList(List<string> thingListNames)
        {
            List<ThingDef> result = new List<ThingDef>();
            thingListNames.Sort(); // 先排序
            
            // 生成缓存键
            string cacheKey = string.Join(",", thingListNames.ToArray());
            if (thingListCache.ContainsKey(cacheKey))
            {
                return thingListCache[cacheKey];
            }

            foreach (var thingListName in thingListNames)
            {
                var thingList = DefDatabase<ItemList>.GetNamed(thingListName);
                if (thingList != null)
                {
                    result.AddRange(thingList.things);
                }
            }
            return result;
        }

        public static void Init() { }

        static DataOf()
        {
            // 初始化
            allDefPartHurts = DefDatabase<DefPartHurtLevel>.AllDefsListForReading;
            allDamageDefs = DefDatabase<DamageDef>.AllDefsListForReading;
            allItemLists = DefDatabase<ItemList>.AllDefsListForReading;
            DefCultivationLines = DefDatabase<DefZdCultivationLine>.AllDefsListForReading;

            #region Debug
            Log.Message("NzRimImmortalBizarre: Aj. got DefPartHurtLevel: " + allDefPartHurts.Count);
            Log.Message("NzRimImmortalBizarre: Zd. got DefCultivationLines: " + DefCultivationLines.Count);
            Log.Message("NzRimImmortalBizarre: Zd. got allItemLists: " + allItemLists.Count);
            Log.Message("NzRimImmortalBizarre: Zd. got allDamageDefs: " + allDamageDefs.Count);
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

            // 所有的修行仿生体
            foreach (var def in DefCultivationLines)
            {
                DefCultivationLineDict.Add(def.defName, def);
                DefCultivationLineDictByLine.Add(def.lineName, def);
            }

        }
    }
}