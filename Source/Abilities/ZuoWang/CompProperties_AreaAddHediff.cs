using RimWorld;
using System;
using System.Linq;
using Verse;



namespace NzRimImmortalBizarre
{
    // CompProperties_AbilityGiveHediff
    // CompAbilityEffect_GiveHediff
    // CompProperties_AbilityNeuroquake
    // CompAbilityEffect_Neuroquake

    public class CompProperties_AreaAddHediff : CompProperties_AbilityEffect
    {

        public HediffDef hediffDef;

        public string target;

        // 仅对自己生效
        public const string TargetSelf = "self";
        // 这张地图上的所有Pawn
        public const string TargetMap = "map";
        // 被选择的人员列表(需要前置的comp实现 @see PawnsSelector)
        public const string TargetSelected = "selected";

        // 自己
        public bool noSelf;

        // 是否筛选
        public bool noFilter;

        // 派系目标

        public bool toEnemies;

        public bool toAllies;

        public bool toNeutral;

        public bool toColonists;

        // 种类目标

        // 对动物生效
        public bool toAnimals;

        // 对人类生效
        public bool toHuman;

        // 对机械生效
        public bool toMechanoids;

        public bool notBoss;

        // 对异常实体生效
        // public bool ApplyToAnomaly;
    }
}