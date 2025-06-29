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
            if (originalPawn == null || map == null)
            {
                duplicatePawn = null;
                return false;
            }

            if (originalPawn.RaceProps.Humanlike)
            {
                duplicatePawn = Find.PawnDuplicator.Duplicate(originalPawn);
                if (faction != null)
                {
                    duplicatePawn.SetFaction(faction);
                }
            }
            else
            {
                duplicatePawn = DuplicateNotHuman(originalPawn);
            }

            map.effecterMaintainer.AddEffecterToMaintain(EffecterDefOf.Skip_EntryNoDelay.Spawn(targetCell, map), targetCell, 60);
            SoundDefOf.Psycast_Skip_Entry.PlayOneShot(new TargetInfo(targetCell, map));

            Find.PawnDuplicator.AddDuplicate(originalPawn.duplicate.duplicateOf, originalPawn);
            Find.PawnDuplicator.AddDuplicate(duplicatePawn.duplicate.duplicateOf, duplicatePawn);
            GenSpawn.Spawn(duplicatePawn, targetCell, map);
            return duplicatePawn != null;
        }

        public static Pawn DuplicateNotHuman(Pawn pawn)
        {
            float ageBiologicalYearsFloat = pawn.ageTracker.AgeBiologicalYearsFloat;
            float num = pawn.ageTracker.AgeChronologicalYearsFloat;
            if (num > ageBiologicalYearsFloat)
            {
                num = ageBiologicalYearsFloat;
            }

            PawnGenerationRequest request = new PawnGenerationRequest(pawn.kindDef, pawn.Faction, PawnGenerationContext.NonPlayer, -1, forceGenerateNewPawn: true,
             allowDead: false, allowDowned: false, canGeneratePawnRelations: false, mustBeCapableOfViolence: false, 0f, forceAddFreeWarmLayerIfNeeded: false,
              allowGay: true, allowPregnant: false, allowFood: true, allowAddictions: true, inhabitant: false, certainlyBeenInCryptosleep: false,
               forceRedressWorldPawnIfFormerColonist: false, worldPawnFactionDoesntMatter: false, 0f, 0f, null, 0f, null, null, null, null, null,
                fixedGender: pawn.gender, fixedBiologicalAge: ageBiologicalYearsFloat, fixedChronologicalAge: num, forceNoIdeo: false, forceNoBackstory: false, forbidAnyTitle: true,
                 forceDead: false, forcedXenogenes: null, forcedEndogenes: null, forcedXenotype: null, allowedXenotypes: null,
                  forceBaselinerChance: 0f, developmentalStages: DevelopmentalStage.Adult, pawnKindDefGetter: null,
                   excludeBiologicalAgeRange: null, biologicalAgeRange: null, forceRecruitable: false, dontGiveWeapon: false,
                    onlyUseForcedBackstories: false, maximumAgeTraits: -1, minimumAgeTraits: 0,
                    forceNoGear: true);

            request.IsCreepJoiner = pawn.IsCreepJoiner;
            request.ForceNoIdeoGear = true;
            request.CanGeneratePawnRelations = false;
            request.DontGivePreArrivalPathway = true;

            Pawn pawn2 = PawnGenerator.GeneratePawn(request);

            int duplicateOf = (pawn.duplicate.duplicateOf == int.MinValue) ? pawn.thingIDNumber : pawn.duplicate.duplicateOf;
            pawn.duplicate.duplicateOf = duplicateOf;
            pawn2.duplicate.duplicateOf = duplicateOf;
            pawn2.Name = NameTriple.FromString(pawn.Name.ToString());

            CopyHediffs(pawn, pawn2);
            CopyNeeds(pawn, pawn2);
            CopyAbilities(pawn, pawn2);

            if (pawn.guest != null)
            {
                pawn2.guest.Recruitable = pawn.guest.Recruitable;
            }

            pawn2.Notify_DuplicatedFrom(pawn);
            pawn2.Drawer.renderer.SetAllGraphicsDirty();
            pawn2.Notify_DisabledWorkTypesChanged();
            return pawn2;
        }

        private static void CopyHediffs(Pawn pawn, Pawn newPawn)
        {
            newPawn.health.hediffSet.hediffs.Clear();
            List<Hediff> hediffs = pawn.health.hediffSet.hediffs;
            foreach (Hediff item in hediffs)
            {
                if (item.def.duplicationAllowed && (item.Part == null || newPawn.health.hediffSet.HasBodyPart(item.Part)) && (!(item is Hediff_AddedPart) || item.def.organicAddedBodypart) && (!(item is Hediff_Implant) || item.def.organicAddedBodypart))
                {
                    Hediff hediff = HediffMaker.MakeHediff(item.def, newPawn, item.Part);
                    hediff.CopyFrom(item);
                    newPawn.health.hediffSet.AddDirect(hediff);
                }
            }

            foreach (Hediff item2 in hediffs)
            {
                if (item2 is Hediff_AddedPart && !item2.def.organicAddedBodypart)
                {
                    newPawn.health.RestorePart(item2.Part, null, checkStateChange: false);
                }
            }
        }

        private static void CopyNeeds(Pawn pawn, Pawn newPawn)
        {
            newPawn.needs.AllNeeds.Clear();
            foreach (Need allNeed in pawn.needs.AllNeeds)
            {
                Need need = (Need)Activator.CreateInstance(allNeed.def.needClass, newPawn);
                need.def = allNeed.def;
                newPawn.needs.AllNeeds.Add(need);
                need.SetInitialLevel();
                need.CurLevel = allNeed.CurLevel;
                newPawn.needs.BindDirectNeedFields();
            }

            if (pawn.needs.mood == null)
            {
                return;
            }

            List<Thought_Memory> memories = newPawn.needs.mood.thoughts.memories.Memories;
            memories.Clear();
            foreach (Thought_Memory memory in pawn.needs.mood.thoughts.memories.Memories)
            {
                Thought_Memory thought_Memory = (Thought_Memory)ThoughtMaker.MakeThought(memory.def);
                thought_Memory.CopyFrom(memory);
                thought_Memory.pawn = newPawn;
                memories.Add(thought_Memory);
            }
        }

        private static void CopyAbilities(Pawn pawn, Pawn newPawn)
        {
            if (pawn.abilities == null || newPawn.abilities == null)
            {
#if DEBUG
                Log.Warning("NzRimImmortalBizarre.Maker.CopyAbilities: pawn.abilities or newPawn.abilities is null");
#endif
                return;
            }
            foreach (Ability ability2 in pawn.abilities.abilities)
            {
                if (newPawn.abilities.GetAbility(ability2.def) == null)
                {
                    newPawn.abilities.GainAbility(ability2.def);
                }
            }

            List<Ability> abilities = newPawn.abilities.abilities;
            for (int num = abilities.Count - 1; num >= 0; num--)
            {
                Ability ability = abilities[num];
                if (pawn.abilities.GetAbility(ability.def) == null)
                {
                    newPawn.abilities.RemoveAbility(ability.def);
                }
            }
        }


    }


}