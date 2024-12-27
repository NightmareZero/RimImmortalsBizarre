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
        public List<Hediff> low;

        // 高阶仿生体
        public List<Hediff> high;
    }
}