using Verse;
using RimWorld;
using UnityEngine;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public class CompProperties_DyeingEvil : CompProperties
    {
        public CompProperties_DyeingEvil()
        {
            compClass = typeof(Comp_DyeingEvil);
        }
    }

    public class Comp_DyeingEvil : ThingComp
    {
        public CompProperties_DyeingEvil Props => (CompProperties_DyeingEvil)props;

        public override void PostExposeData()
        {
            if (Scribe.mode == LoadSaveMode.PostLoadInit)
            {
                // TODO 测试一下没有Def定义，纯手工添加时能否调用
                // Initialize any properties or fields if necessary
                Log.Warning("Comp_DyeingEvil PostLoadInit called for " + parent.Label);
            }
        }
        public override string CompTipStringExtra()
        {
            return "NzRI_Bj_Thing_DyeingEvil".Translate();
        }

        public override string CompInspectStringExtra()
        {
            return "NzRI_Bj_Thing_DyeingEvil".Translate();
        }


    }
}