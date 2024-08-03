using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class DefPartHurt : Def
    {
        public List<DefPartHurtLevel> levels;     
    }

    public class DefPartHurtLevel : Def
        {
            public int level;
            public string group;

            // 可用身体部件列表 defName
            public List<BodyPartDef> bodyParts;

            // private List<BodyPartDef> caches;
            // public List<BodyPartDef> BodyParts
            // {
            //     get
            //     {
            //         // 如果缓存为空，则进行初始化
            //         if (caches == null)
            //         {
            //             caches = new List<BodyPartDef>();

            //             // 遍历 bodyParts 列表中的每一个字符串
            //             foreach (string part in bodyParts)
            //             {
            //                 // 尝试从 DefDatabase 中获取对应的 BodyPartDef 对象
            //                 BodyPartDef bodyPartDef = DefDatabase<BodyPartDef>.GetNamedSilentFail(part);

            //                 // 如果找到了对应的 BodyPartDef 对象，则将其添加到缓存列表中
            //                 if (bodyPartDef != null)
            //                 {
            //                     caches.Add(bodyPartDef);
            //                 }
            //             }
            //         }

            //         // 返回缓存的 BodyPartDef 对象列表
            //         return caches;
            //     }
            // }
        }
}