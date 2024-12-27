using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{ 
    public class DefCultivationBodyPartList : Def
    {
        // 获得高阶需要的低阶仿生体最小数量
        public int minLowCount;

        // 列表标签
        public List<TagDef> tags;

        // 低阶仿生体
        public List<HediffDef> low;

        // 高阶仿生体
        public List<HediffDef> high;

        public List<string> TagNames
        {
            get
            {
                if (_tagNamesCache == null)
                {
                    _tagNamesCache = new List<string>();
                    foreach (var tag in tags)
                    {
                        _tagNamesCache.Add(tag.defName);
                    }
                }
                return _tagNamesCache;
            }
        }

        private List<string> _tagNamesCache = null;
    }
}