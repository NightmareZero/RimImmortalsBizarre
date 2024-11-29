using System.Text.RegularExpressions;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Noise;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    {
        /// <summary>
        /// 对Pawn造成身体部件伤害，伤害部位由组别和损毁等级决定，优先对受伤害最小的部位造成伤害
        /// </summary>
        /// <param name="pawn">目标Pawn</param>
        /// <param name="Group">组别</param>
        /// <param name="level">损毁等级</param>
        /// <param name="min">伤害最小值</param>
        /// <param name="max">伤害最大值</param
        public static void DamageBodyPart(this Pawn pawn, string Group, int level, int min, int max)
        {
            // 获取符合要求的BodyPartDefs
            var bodyPartDefs = GetPartHurtLevel(Group, level).bodyParts;

            // 获取所有不缺失的身体部件
            var notMissingParts = pawn.health.hediffSet.GetNotMissingParts();

            // 找到受伤害最小的符合要求的部件
            BodyPartRecord targetPart = notMissingParts
                .Where(part => bodyPartDefs.Contains(part.def))
                .OrderBy(part => part.def.hitPoints - pawn.health.hediffSet.GetPartHealth(part))
                .FirstOrDefault();

            // 如果找到了目标部件，则对其造成伤害
            if (targetPart != null)
            {
                pawn.TakeDamage(new DamageInfo(DamageDefOf.SurgicalCut, Rand.Range(min, max), 999f, -1f, null, targetPart));
            }
            else
            {
                Log.Message("Nz_AoJing_HurtSelf_NoBodyPart".Translate(pawn.Name.ToStringShort));
            }
        }

        /// <summary>
        /// 移除Pawn的身体部位
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="part"></param>
        public static void RemoveBodyPart(Pawn pawn, BodyPartRecord part)
        {
            if (pawn == null || part == null)
            {
                return;
            }

            // 创建一个表示身体部位缺失的 Hediff
            Hediff_MissingPart missingPart = (Hediff_MissingPart)HediffMaker.MakeHediff(HediffDefOf.MissingBodyPart, pawn, part);
            missingPart.lastInjury = HediffDefOf.SurgicalCut;
            missingPart.IsFresh = true;

            // 添加到 Pawn 的健康状态中
            pawn.health.AddHediff(missingPart, part);
        }

        /// <summary>
        /// 判断Pawn是否有可受伤的身体部位
        /// </summary>
        /// <param name="pawn">目标Pawn</param>
        /// <param name="Group">组别</param>
        /// <param name="level">损毁等级</param>
        /// <returns>是否有可受伤的身体部位</returns>
        public static bool HasCanDamageBodyPart(this Pawn pawn, string Group, int level)
        {
            // 获取符合要求的BodyPartDefs
            var bodyPartDefs = GetPartHurtLevel(Group, level).bodyParts;

            // 获取所有不缺失的身体部件
            var notMissingParts = pawn.health.hediffSet.GetNotMissingParts();

            // 找到符合要求的部件
            BodyPartRecord targetPart = notMissingParts
                .Where(part => bodyPartDefs.Contains(part.def))
                .First();


            return targetPart != null;
        }

        public static DefPartHurtLevel GetPartHurtLevel(string group, int level)
        {

            foreach (DefPartHurtLevel DefPartHurtLevel in DataOf.allDefPartHurts)
            {
                if (DefPartHurtLevel.group == group && DefPartHurtLevel.level == level)
                {
                    return DefPartHurtLevel;
                }
            }
            return null;
        }


        /// <summary>
        /// 获取Pawn的心情值
        /// </summary>
        /// <param name="pawn">目标Pawn</param>
        /// <returns>心情值</returns
        public static float GetPawnMoods(this Pawn pawn)
        {
            if (pawn == null || pawn.needs == null || pawn.needs.mood == null)
            {
                return 0;
            }
            return pawn.needs.mood.CurLevel;
        }

        /// <summary>
        /// 获取Pawn的疼痛值
        /// </summary>
        /// <param name="pawn">目标Pawn</param>
        /// <returns>疼痛值</returns>
        public static float GetPawnPain(this Pawn pawn)
        {
            if (pawn == null || pawn.health == null || pawn.health.hediffSet == null)
            {
                return 0;
            }
            return pawn.health.hediffSet.PainTotal;
        }
    }

}