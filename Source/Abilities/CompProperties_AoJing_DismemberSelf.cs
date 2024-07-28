using RimWorld;


namespace NzRimImmortalBizarre
{
    public class CompProperties_AoJing_DismemberSelf : CompProperties_AbilityEffect
    {
        // 损毁等级
        public int level;

        // 组别
        public string group;

        // 伤害默认值为 1 ~ 2
        public int damageMin = 1;
        public int damageMax = 2;
        public CompProperties_AoJing_DismemberSelf()
        {
            this.compClass = typeof(CompAbilityEffect_AoJing_DismemberSelf);
        }
    }
}