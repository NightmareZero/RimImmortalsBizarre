using RimWorld;
using System.Linq;



namespace NzRimImmortalBizarre
{
    public class CompAbilityEffect_ZuoWangBase : CompAbilityEffect
    {

        protected bool GetCastSuccess()
        {
            // 查找comps中comp的每一个prop是否有IsCastingFailable的子类，返回所有子类
            var fail = parent.comps
                .Where(comp => comp.props is IsCastingFailable)
                .Select(comp => comp.props as IsCastingFailable)
                // 查找是否有一个子类的IsCastingSuccess为false
                .Any(f => f != null && !f.IsCastingSuccess());
        
            return !fail;
        }
    }
}