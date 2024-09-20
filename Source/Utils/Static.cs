using System.Text.RegularExpressions;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Noise;
using System.Collections.Generic;

namespace NzRimImmortalBizarre
{
    public static partial class StaticUtils
    {
        public static bool GetCastSuccess(this CompAbilityEffect effect)
        {
            // 查找comps中comp的每一个prop是否有IsCastingFailable的子类，返回所有子类
            var fail = effect.parent.comps
                .Where(comp => comp.props is IsCastingFailable)
                .Select(comp => comp.props as IsCastingFailable)
                // 查找是否有一个子类的IsCastingSuccess为false
                .Any(f => f != null && !f.IsCastingSuccess());
        
            return !fail;
        }


    }
}