using System.Text.RegularExpressions;
using System.Linq;
using RimWorld;
using Verse;
using Verse.Noise;
using System.Collections.Generic;
using System;
using Verse.Sound;

namespace NzRimImmortalBizarre
{
    public static class Maker
    {
        public static bool TryDuplicatePawn(Pawn originalPawn, IntVec3 targetCell, Map map, out Pawn duplicatePawn, Faction faction = null)
        {

            duplicatePawn = Find.PawnDuplicator.Duplicate(originalPawn);
            if (faction != null)
            {
                duplicatePawn.SetFaction(faction);
            }

            map.effecterMaintainer.AddEffecterToMaintain(EffecterDefOf.Skip_EntryNoDelay.Spawn(targetCell, map), targetCell, 60);
            SoundDefOf.Psycast_Skip_Entry.PlayOneShot(new TargetInfo(targetCell, map));

            Find.PawnDuplicator.AddDuplicate(originalPawn.duplicate.duplicateOf, originalPawn);
            Find.PawnDuplicator.AddDuplicate(duplicatePawn.duplicate.duplicateOf, duplicatePawn);
            GenSpawn.Spawn(duplicatePawn, targetCell, map);
            return duplicatePawn != null;
        }
    }
}