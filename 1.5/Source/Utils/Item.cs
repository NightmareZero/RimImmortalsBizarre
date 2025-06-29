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
        public static bool IsItemParentCategory(this ThingDef thingDef, ThingCategoryDef isCategory)
        {
            if (thingDef.thingCategories == null)
            {
                #if DEBUG
                Log.Warning("IsItemParentCategory: 'isCategory' is null");
                #endif
                return false;
            }

            if (thingDef.thingCategories == null)
            {
                return false;
            }

            foreach (var category in thingDef.thingCategories)
            {
                if (IsParentCategory(category,isCategory))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsThingHasParentCategory(ThingDef thingDef, ThingCategoryDef hasCategory)
        {
            if (thingDef.thingCategories == null)
            {
                return false;
            }

            foreach (var category in thingDef.thingCategories)
            {
                if (category == hasCategory || IsParentCategory(category, hasCategory))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsParentCategory(ThingCategoryDef category,ThingCategoryDef isCategory)
        {
            if (isCategory == null)
            {
#if DEBUG
                Log.Warning("IsParentCategory: isCategory is null");
#endif
                return false;
            }

            if (category == null)
            {
#if DEBUG
                Log.Warning("IsParentCategory:" + isCategory.defName + " targetCategory is null");
#endif
                return false;
            }


            if (isCategory.defName == category.defName)
            {
                return true;
            }

            if (category.parent != null)
            {
                return IsParentCategory(category.parent, isCategory);
            }

            return false;
        }
    }
}