using Verse;
using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using System;
using WhoXiuXian.Abilities;

using Core;
using WhoXiuXian;


namespace NzRimImmortalBizarre
{ 
    public interface IsCastingFailable
    {
        bool IsCastingSuccess();
    }

    public interface PawnsSelector
    {
        List<Pawn> GetSelectedPawns();
    }

}