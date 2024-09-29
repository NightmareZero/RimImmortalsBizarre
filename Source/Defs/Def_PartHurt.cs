using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public class DefPartHurtLevel : Def
    {

        public int level;
        public string group;

        // 可用身体部件列表 defName
        public List<BodyPartDef> bodyParts;
    }
}