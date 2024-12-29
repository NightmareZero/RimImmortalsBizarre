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
        public static List<DefPartHurtLevel> allDefPartHurts => DefDatabase<DefPartHurtLevel>.AllDefsListForReading;
        public static Dictionary<string, DefPartHurtLevel> allDefPartHurtsDict = new Dictionary<string, DefPartHurtLevel>();

        // 所有的伤害类型 DamageDef
        public static List<DamageDef> allDamageDefs => DefDatabase<DamageDef>.AllDefsListForReading;
        public static Dictionary<string, DamageDef> allDamageDefsDict = new Dictionary<string, DamageDef>();

        // 所有的物品清单 ThingList
        public static List<ThingList> allThingLists => DefDatabase<ThingList>.AllDefsListForReading;
        public static Dictionary<string, List<ThingDef>> thingListCache = new Dictionary<string, List<ThingDef>>();

        // 正德寺修行路线
        public static List<DefZdCultivationLine> DefCultivationLines => DefDatabase<DefZdCultivationLine>.AllDefsListForReading;
        // 正德寺修行路线 DefName=> DefZdCultivationLine
        public static Dictionary<string, DefZdCultivationLine> DefCultivationLineDict = new Dictionary<string, DefZdCultivationLine>();
        // 正德寺修行路线 LineName=> DefZdCultivationLine
        public static Dictionary<string, DefZdCultivationLine> DefCultivationLineDictByLine = new Dictionary<string, DefZdCultivationLine>();

        public static void Init() { }

        public static List<ThingDef> GetCachedThingList(List<ThingList> thingLists)
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

        public static List<ThingDef> GetCachedThingList(List<string> thingListNames)
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
                var thingList = DefDatabase<ThingList>.GetNamed(thingListName);
                if (thingList != null)
                {
                    result.AddRange(thingList.things);
                }
            }
            return result;
        }

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

            // 所有的修行仿生体
            foreach (var def in DefCultivationLines)
            {
                DefCultivationLineDict.Add(def.defName, def);
                DefCultivationLineDictByLine.Add(def.lineName, def);
            }

        }
    }
}