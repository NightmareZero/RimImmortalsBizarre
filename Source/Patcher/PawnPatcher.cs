using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    public interface PawnPatcher
    {
        void PatchPawn(Pawn pawn);
    }

    public static class CopyJunkPawnPatcher
    {
        public static void PatchPawn(Pawn pawn)
        {
            if (pawn == null)
            {
                Log.Error("NzRimImmortalBizarre.CopyJunkPawnPatcher.PatchPawn: pawn is null");
                return;
            }

            // 获取未缺失的身体部位
            List<BodyPartRecord> bodyParts = pawn.health.hediffSet.GetNotMissingParts().ToList();

            // 如果有大于1个肺，移除一个
            RemoveBodyPartRecordOnDouble(pawn, bodyParts, BodyPartDefOf.Lung);

            // 如果有大于1腿
            RemoveBodyPartRecordOnDouble(pawn, bodyParts, BodyPartDefOf.Leg);

            // 如果有大于1胳膊
            RemoveBodyPartRecordOnDouble(pawn, bodyParts, BodyPartDefOf.Arm);

            // 包扎所有流血的伤口
            List<Hediff_Injury> hediffInjuries = new List<Hediff_Injury>();
            pawn.health.hediffSet.GetHediffs(ref hediffInjuries, null);
            foreach (Hediff_Injury hediff in hediffInjuries)
            {
                if (hediff.Bleeding)
                {
                    hediff.Tended(99999f, 99999f);
                }
            }


        }

        public static void RemoveBodyPartRecordOnDouble(Pawn pawn, List<BodyPartRecord> bodyParts, BodyPartDef bodyPartDef)
        {
            if (pawn == null)
            {
                Log.Error("NzRimImmortalBizarre.CopyJunkPawnPatcher.GetRemovableBodyPartRecord: pawn is null");
                return;
            }

            if (bodyPartDef == null)
            {
                Log.Error("NzRimImmortalBizarre.CopyJunkPawnPatcher.GetRemovableBodyPartRecord: bodyPartDef is null");
                return;
            }

            BodyPartRecord bodyPartRecord = pawn.health.hediffSet.GetNotMissingParts().FirstOrDefault(x => x.def == bodyPartDef);
            if (bodyPartRecord == null)
            {
#if DEBUG
                Log.Warning("NzRimImmortalBizarre.CopyJunkPawnPatcher.GetRemovableBodyPartRecord: bodyPartRecord is null");
#endif
                return;
            }

            // 如果有大于1个，移除一个
            List<BodyPartRecord> lung = bodyParts.FindAll(x => x.def == bodyPartDef);
            if (lung != null && lung.Count > 1)
            {
                BodyPartRecord lungToRemove = lung[0];
                //  替换为部位缺失
                pawn.health.AddHediff(HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, lungToRemove));
            }

        }
    }
}