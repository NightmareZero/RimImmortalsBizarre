using System.Text.RegularExpressions;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Noise;

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
            var bodyPartDefs = XmlOf.defDismemberSelf.GetLevel(Group, level).GetBodyPartDefs();

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
            var bodyPartDefs = XmlOf.defDismemberSelf.GetLevel(Group, level).GetBodyPartDefs();

            // 获取所有不缺失的身体部件
            var notMissingParts = pawn.health.hediffSet.GetNotMissingParts();

            // 找到受伤害最小的符合要求的部件
            BodyPartRecord targetPart = notMissingParts
                .Where(part => bodyPartDefs.Contains(part.def))
                .FirstOrDefault();

            return targetPart != null;
        }
    }

}