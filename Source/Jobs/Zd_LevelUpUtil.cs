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

        public static bool LevelUpAndAddBodyPart(Pawn pawn, DefZdCultivationLine bodyPartList,
        out Hediff addedHediff, out BodyPartRecord installOnBodyPart)
        {
            try
            {
                // 根据标签，获取Pawn所有的仿生体
                List<Hediff> bodyParts = pawn.GetHediffByTags(bodyPartList.TagNames.ToArray());

                // 可以添加的仿生体
                List<HediffDef> canAddHediffDefs = new List<HediffDef>();
                if (bodyParts.Count >= bodyPartList.minLowCount)
                {
                    // 低级部位数量达到要求
                    canAddHediffDefs.AddRange(bodyPartList.high);
                }
                else
                {
                    // 低级部位数量未达到要求
                    canAddHediffDefs.AddRange(bodyPartList.low);
                }

                // 将已有的仿生体排除
                canAddHediffDefs = canAddHediffDefs.Except(bodyParts.Select(x => x.def)).ToList();

                while (canAddHediffDefs.Count > 0)
                {
                    // 随机获取一个可以添加的仿生体
                    HediffDef hediffDef = canAddHediffDefs.RandomElement();
                    // 检查对应的部位是否都被占用了

                    if (!isHediffBodyPartCanUse(pawn, hediffDef.defaultInstallPart, out installOnBodyPart))
                    {
                        // 跳过, 尝试下一个
                        canAddHediffDefs.Remove(hediffDef);
                        continue;
                    }
                    addedHediff = HediffMaker.MakeHediff(hediffDef, pawn,installOnBodyPart);
                    // 添加仿生体
                    pawn.health.AddHediff(addedHediff);
                    return true;
                }

            }
            catch (Exception e)
            {
                e.PrintExceptionWithStackTrace();
            }
            installOnBodyPart = null;
            addedHediff = null;
            return false;
        }


        /// <summary>
        /// 获取是否有可用的可安装的身体部位
        /// 1. 如果部位缺失, 返回true
        /// 2. 根据部位寻找是否有可用的部位, 有则true
        /// 3. 返回false
        /// </summary>
        /// <param name="pawn"></param>
        /// <param name="bodyPartDef"></param>
        /// <returns></returns>
        public static bool isHediffBodyPartCanUse(Pawn pawn, BodyPartDef bodyPartDef,out BodyPartRecord gotBodyPart)
        {
            gotBodyPart = null;
            if (pawn?.health?.hediffSet == null)
            {
                Log.Error($"pawn {pawn.Name} health hediffSet is null");
                return false;
            }

            // 如果是一个缺失部位, 则返回true
            var missingPart = pawn.health.hediffSet.GetMissingPartsCommonAncestors();
            foreach (var part in missingPart)
            {
                if (bodyPartDef.defName == part.Part.def.defName)
                {
                    #if DEBUG
                    Log.Message($"pawn {pawn.Name} got missing bodyPartDef {bodyPartDef.defName}");
                    #endif
                    gotBodyPart = part.Part;
                    return true;
                }
            }

            #if DEBUG
            Log.Message($"pawn {pawn.Name} not found missing bodyPartDef {bodyPartDef.defName}");
            #endif

            // 如果所有的该部位都已经占用, 则返回false
            var allParts = pawn.health.hediffSet.GetNotMissingParts();
            List<BodyPartRecord> bodyPartRecords = new List<BodyPartRecord>();
            foreach (var part in allParts)
            {
                if (part.def.defName == bodyPartDef.defName)
                {
                    bodyPartRecords.Add(part);
                }
            }
            // 如果没有找到该部位, 则返回false
            if (bodyPartRecords.Count == 0)
            {
                #if DEBUG
                Log.Message($"pawn {pawn.Name} not found bodyPartDef {bodyPartDef.defName}");
                #endif
                return false;
            }
            // 对现有的Hediff进行匹配
            foreach (var bodyPart in bodyPartRecords)
            { 
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffMatchingPart<Hediff_AddedPart>(bodyPart);
                if (hediff == null)
                {
                    #if DEBUG
                    Log.Message($"pawn {pawn.Name} found bodyPartDef {bodyPartDef.defName} is not used");
                    #endif
                    gotBodyPart = bodyPart;
                    return true;
                }
            }
            return false;

        }
    }
}