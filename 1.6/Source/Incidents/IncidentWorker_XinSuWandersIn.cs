using System.Collections.Generic;
using System.Linq;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{
    // IncidentWorker_WildManWandersIn
    public class IncidentWorker_XinSuWandersIn : IncidentWorker
    {
        protected override bool CanFireNowSub(IncidentParms parms)
        {
            if (!base.CanFireNowSub(parms))
            {
                return false;
            }

            if (!Utils.TryFindFormerFaction(out var _))
            {
                return false;
            }

            // 是否有有毒尘埃事件
            Map map = (Map)parms.target;
            if (map.GameConditionManager.ConditionIsActive(GameConditionDefOf.ToxicFallout))
            {
                return false;
            }

            // 是否有生物科技的剧毒烟雾事件
            if (ModsConfig.BiotechActive && map.GameConditionManager.ConditionIsActive(GameConditionDefOf.NoxiousHaze))
            {
                return false;
            }

            // 检查地图的温度是否适合人类
            if (!map.mapTemperature.SeasonAcceptableFor(ThingDefOf.Human))
            {
                return false;
            }

            IntVec3 cell;
            return Utils.TryFindEntryCell(map, out cell);
        }

        protected override bool TryExecuteWorker(IncidentParms parms)
        {
            Map map = (Map)parms.target;
            if (!Utils.TryFindEntryCell(map, out var cell))
            {
                return false;
            }

            if (!Utils.TryFindFormerFaction(out var formerFaction))
            {
                return false;
            }

            // 生成野生心素
            Pawn pawn = GenerateWildManPawn(formerFaction, cell, map);
            PatchPawnAsXinSu(pawn);

            // 输出消息
            TaggedString taggedString = "Person".Translate();
            TaggedString title = def.letterLabel.Formatted(pawn.KindLabel).CapitalizeFirst();
            TaggedString text2 = def.letterText.Formatted(pawn.NameShortColored, taggedString, pawn.Named("PAWN")).AdjustedFor(pawn).CapitalizeFirst();
            PawnRelationUtility.TryAppendRelationsWithColonistsInfo(ref text2, ref title, pawn);
            SendStandardLetter(title, text2, def.letterDef, parms, pawn);
            return true;
        }

        /// <summary>
        /// 生成一个普通野人
        /// </summary>
        /// <param name="formerFaction"></param>
        /// <param name="cell"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private Pawn GenerateWildManPawn(Faction formerFaction, IntVec3 cell, Map map)
        {
            // 必须是成年人
            DevelopmentalStage developmentalStages = DevelopmentalStage.Adult;
            Pawn pawn = PawnGenerator.GeneratePawn(new PawnGenerationRequest(PawnKindDefOf.WildMan, null, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: false, allowDead: false, allowDowned: false, canGeneratePawnRelations: true, mustBeCapableOfViolence: false, 1f, forceAddFreeWarmLayerIfNeeded: false, allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false, forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 1f, null, null, null, null, null, null, null, null, null, null, null, null, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: false, forceDead: false, null, null, null, null, null, 0f, developmentalStages));
            pawn.SetFaction(null);
            GenSpawn.Spawn(pawn, cell, map);
            return pawn;
        }

        /// <summary>
        /// 将野人转换为心素
        /// </summary>
        /// <param name="pawn"></param>
        private void PatchPawnAsXinSu(Pawn pawn)
        {

            List<BodyPartRecord> parts = pawn.health.hediffSet.GetNotMissingParts().ToList(); // 身体部位

            foreach (var part in parts)
            {
                // 安装心素之眼
                if (part.def == BodyPartDefOf.Eye)
                {
                    // 创建并添加心素仿生眼睛的 Hediff
                    Hediff hediffEye = HediffMaker.MakeHediff(BodyPartDef1Of.NzRI_XinSu_Eye, pawn, part);
                    pawn.health.AddHediff(hediffEye);
                    continue;
                }

                if (part.def == BodyPartDefOf.Heart)
                {
                    // TODO
                }
            }



        }


    }
}