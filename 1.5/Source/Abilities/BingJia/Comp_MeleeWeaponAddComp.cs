using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;



namespace NzRimImmortalBizarre
{
    public class CompProperties_MeleeWeaponAddComp : CompProperties_AbilityEffect
    {
        public CompProperties_DyeingEvil AddComp;

        public CompProperties_MeleeWeaponAddComp()
        {
            compClass = typeof(Comp_MeleeWeaponAddComp);
        }
    }

    public class Comp_MeleeWeaponAddComp : CompAbilityEffect
    {
        public new CompProperties_MeleeWeaponAddComp Props => (CompProperties_MeleeWeaponAddComp)props;

        public override bool Valid(LocalTargetInfo target, bool throwMessages = false)
        {
            var baseRs = base.Valid(target, throwMessages);
            if (!baseRs)
            {
                return false;
            }

            // 检查目标的Thing是否是近战武器
            if (target.Thing == null)
            {
                if (throwMessages)
                {
                    Messages.Message("NzRI_Bj_Thing_MeleeWeaponAddComp_NoTarget".Translate(), MessageTypeDefOf.RejectInput);
                }
                return false;
            }

            var ThingDef = target.Thing.def;
            if (!ThingDef.IsMeleeWeapon)
            {
                if (throwMessages)
                {
                    Messages.Message("NzRI_Bj_Thing_MeleeWeaponAddComp_NotMeleeWeapon".Translate(), MessageTypeDefOf.RejectInput);
                }
                return false;
            }

            return true;
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (target.Thing is ThingWithComps thingWithComps)
            {
                Comp_DyeingEvil compDyeingEvil = new Comp_DyeingEvil();
                compDyeingEvil.parent = thingWithComps;
                compDyeingEvil.Initialize(Props.AddComp);
                // 可选：调用初始化方法
                compDyeingEvil.PostSpawnSetup(false);

                thingWithComps.AllComps.Add(compDyeingEvil);

                // TODO 播放染煞的声音
            }
        }

        

        public override string CompInspectStringExtra()
        {
            return "NzRI_Bj_Thing_MeleeWeaponAddComp".Translate();
        }
    }
}