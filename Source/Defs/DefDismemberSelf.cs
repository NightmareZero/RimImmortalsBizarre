using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class DefDismemberSelf : Def
    {
        public List<DefDismemberSelfLevel> levels;

        public DefDismemberSelfLevel GetLevel(string group, int level)
        {
            foreach (DefDismemberSelfLevel defDismemberSelfLevel in levels)
            {
                if (defDismemberSelfLevel.group == group && defDismemberSelfLevel.level == level)
                {
                    return defDismemberSelfLevel;
                }
            }

            return null;
        }
    }

    public class DefDismemberSelfLevel
    {
        public int level;
        public string group;

        // 可用身体部件列表 defName
        public List<string> parts;

        private List<BodyPartDef> caches;


        public List<BodyPartDef> GetBodyPartDefs()
        {
            // 如果缓存为空，则进行初始化
            if (caches == null)
            {
                caches = new List<BodyPartDef>();

                // 遍历parts列表中的每一个字符串
                foreach (string part in parts)
                {
                    // 尝试从DefDatabase中获取对应的BodyPartDef对象
                    BodyPartDef bodyPartDef = DefDatabase<BodyPartDef>.GetNamedSilentFail(part);

                    // 如果找到了对应的BodyPartDef对象，则将其添加到缓存列表中
                    if (bodyPartDef != null)
                    {
                        caches.Add(bodyPartDef);
                    }
                }
            }

            // 返回缓存的BodyPartDef对象列表
            return caches;
        }
    }
}