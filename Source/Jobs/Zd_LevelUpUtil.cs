using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core;
using RimWorld;
using UnityEngine;
using Verse;
using Verse.AI;
using WhoXiuXian;
using WhoXiuXian.Abilities;

namespace NzRimImmortalBizarre
{
    public static class ZdLevelUpUtil
    {

        /// <summary>
        /// 突破并添加仿生体
        /// 其余见 @see PreCreateBodyPart
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="cultivationLine"></param>
        /// <param name="addedHediff"></param>
        /// <param name="installOnBodyPart"></param>
        /// <returns></returns>
        public static bool LevelUpAndAddBodyPart(Pawn pawn, DefZdCultivationLine cultivationLine,
        out Hediff addedHediff, out BodyPartRecord installOnBodyPart)
        {
            bool ok = PreCreateBodyPart(pawn, cultivationLine, out addedHediff, out installOnBodyPart);
            if (ok)
            {
                pawn.health.AddHediff(addedHediff, installOnBodyPart);
            }
            return ok;
        }

        /// <summary>
        /// 预创建一个仿生体和对应的部位
        /// 找到一个可以添加的仿生体，和其对应的部位
        /// </summary>
        /// <param name="pawn">目标</param>
        /// <param name="cultivationLine">突破路线</param>
        /// <param name="targetHediff">输出: 创建好的Hediff</param>
        /// <param name="targetBodyPart">输出: 找到的BodyPart</param>
        /// <returns></returns>
        public static bool PreCreateBodyPart(Pawn pawn, DefZdCultivationLine cultivationLine,
        out Hediff targetHediff, out BodyPartRecord targetBodyPart)
        {
            try
            {
                // 根据标签，获取Pawn所有的仿生体
                List<Hediff> bodyParts = pawn.GetHediffByTags(cultivationLine.TagNames.ToArray());
#if DEBUG
                Log.Message($"pawn {pawn.Name} got installed bodyParts count {bodyParts.Count}");
#endif

                // 可以添加的仿生体
                List<HediffDef> canAddHediffDefs = new List<HediffDef>();
                canAddHediffDefs.AddRange(cultivationLine.low);
                if (bodyParts.Count >= cultivationLine.minLowCount)
                {
                    // 低级部位数量达到要求
                    canAddHediffDefs.AddRange(cultivationLine.high);
                }

                // 将已有的仿生体排除
                canAddHediffDefs = canAddHediffDefs.Except(bodyParts.Select(x => x.def)).ToList();
#if DEBUG
                Log.Message($"pawn {pawn.Name} canAddHediffDefs count {canAddHediffDefs.Count}");
#endif
                if (canAddHediffDefs.Count == 0)
                {
                    // 没有可以添加的仿生体
                    // TODO Message输出
                    targetBodyPart = null;
                    targetHediff = null;
                    return false;
                }

                while (canAddHediffDefs.Count > 0)
                {
                    // 随机获取一个可以添加的仿生体
                    HediffDef hediffDef = canAddHediffDefs.RandomElement();
                    // 检查对应的部位是否都被占用了

                    if (!isHediffBodyPartCanUse(pawn, hediffDef.defaultInstallPart, out targetBodyPart))
                    {
                        // 跳过, 尝试下一个
                        canAddHediffDefs.Remove(hediffDef);
#if DEBUG
                        Log.Message($"pawn {pawn.Name} can not use bodyPartDef {hediffDef.defaultInstallPart.defName}");
#endif
                        continue;
                    }
#if DEBUG
                    Log.Message($"pawn {pawn.Name} can use bodyPartDef {hediffDef.defaultInstallPart.defName}");
#endif
                    targetHediff = HediffMaker.MakeHediff(hediffDef, pawn, targetBodyPart);
                    // 此处不添加仿生体，只是预创建
                    return true;
                }

            }
            catch (Exception e)
            {
                e.PrintExceptionWithStackTrace();
            }
            targetBodyPart = null;
            targetHediff = null;
            return false;
        }


        /// <summary>
        /// 获取是否有可用的可安装的身体部位
        /// 1. 如果部位缺失, 返回true
        /// 2. 根据部位寻找是否有可用的部位, 有则true
        /// 3. 返回false
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="willInstallbodyPartDef"></param>
        /// <returns></returns>
        public static bool isHediffBodyPartCanUse(Pawn pawn, BodyPartDef willInstallbodyPartDef, out BodyPartRecord gotBodyPart)
        {
            gotBodyPart = null;
            if (pawn?.health?.hediffSet == null)
            {
                Log.Error($"pawn {pawn.Name} health hediffSet is null");
                return false;
            }

            // 检查是否有缺失的目标部位
            var missingPart = pawn.health.hediffSet.GetMissingPartsCommonAncestors();
            foreach (var part in missingPart)
            {
                if (willInstallbodyPartDef.defName == part.Part.def.defName)
                {
#if DEBUG
                    Log.Message($"pawn {pawn.Name} got missing bodyPartDef {willInstallbodyPartDef.defName}");
#endif
                    gotBodyPart = part.Part;
                    return true;
                }
            }

#if DEBUG
            Log.Message($"pawn {pawn.Name} not found missing bodyPartDef {willInstallbodyPartDef.defName}");
#endif

            // 获取所有未缺失的目标部位
            var allParts = pawn.health.hediffSet.GetNotMissingParts();
            List<BodyPartRecord> bodyPartRecords = new List<BodyPartRecord>();
            foreach (var part in allParts)
            {
                if (part.def.defName == willInstallbodyPartDef.defName)
                {
                    bodyPartRecords.Add(part);
                }
            }

            // 如果没有找到该部位, 则返回false
            if (bodyPartRecords.Count == 0)
            {
#if DEBUG
                Log.Message($"pawn {pawn.Name} not found bodyPartDef {willInstallbodyPartDef.defName}");
#endif
                return false;
            }

            // 检查是否有未安装仿生体的目标部位
            foreach (var bodyPart in bodyPartRecords)
            {
                bool hasBionicPart = pawn.health.hediffSet.hediffs
                    .OfType<Hediff_AddedPart>()
                    .Any(hediff => hediff.Part == bodyPart);

                if (!hasBionicPart)
                {
#if DEBUG
                    Log.Message($"pawn {pawn.Name} found willInstallbodyPartDef {willInstallbodyPartDef.defName} is not used");
#endif
                    gotBodyPart = bodyPart;
                    return true;
                }
            }

            return false;
        }
    }
}