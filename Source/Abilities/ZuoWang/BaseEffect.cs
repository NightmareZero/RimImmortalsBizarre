using RimWorld;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_ZuoWangBase : CompAbilityEffect
    {

        protected bool GetCheatSuccess()
        {
            // 使用 FirstOrDefault 查找第一个 IsFailable success == false 类型的组件
            var failableComp = parent.comps.OfType<IsFailable>().FirstOrDefault(f => !f.IsCastingSuccess());
            return failableComp == null;
        }

    }
}