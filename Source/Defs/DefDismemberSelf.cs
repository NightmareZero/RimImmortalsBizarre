using System.Collections.Generic;
using RimWorld;
using Verse;

namespace NzRimImmortalBizarre
{ 

    public class DefDismemberSelf : Def
    {
       public List<DefDismemberSelfLevel> levels;
    }

    public class DefDismemberSelfLevel { 
        int level;

        
    }
}