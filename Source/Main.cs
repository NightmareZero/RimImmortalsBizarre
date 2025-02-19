﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using UnityEngine;
using Verse;
using Verse.AI;
using Verse.AI.Group;
using Verse.Sound;
using Verse.Noise;
using Verse.Grammar;
using RimWorld;
using RimWorld.Planet;
using HarmonyLib;
using System.Reflection;

// *Uncomment for Harmony*
// using System.Reflection;
// using HarmonyLib;

namespace NzRimImmortalBizarre
{
    [StaticConstructorOnStartup]
    public static class Start
    {
        static Start()
        {
            Log.Message("RimImmortalsBizarre loaded successfully!");

            Harmony harmony = new Harmony("NzRimImmortalBizarre");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
    [DefOf]
    public class TemplateDefOf
    {
        public static LetterDef success_letter;
    }

    public class MyMapComponent : MapComponent
    {
        public MyMapComponent(Map map) : base(map) { }
        public override void FinalizeInit()
        {
            // Messages.Message("Success", null, MessageTypeDefOf.PositiveEvent);
            // Find.LetterStack.ReceiveLetter(new TaggedString("Success"), new TaggedString("Success message"), TemplateDefOf.success_letter, "", 0);
        }
    }

    public class NzRimImmortalBizarreGameComponent : GameComponent
    {
        public NzRimImmortalBizarreGameComponent(Game game)
        {
        }
        public override void LoadedGame()
        {
            base.LoadedGame();
            Caches.Clear();
        }

        public override void FinalizeInit()
        {
            base.FinalizeInit();

            DataOf.Init();
        }
    }



    // *Uncomment for Harmony*
    // [HarmonyPatch(typeof(LetterStack), "ReceiveLetter")]
    // [HarmonyPatch(new Type[] {typeof(TaggedString), typeof(TaggedString), typeof(LetterDef), typeof(string), typeof(int), typeof(bool)})]
    // public static class LetterTextChange
    // {
    //     public static bool Prefix(ref TaggedString text)
    //     {
    //         text += new TaggedString(" with harmony");
    //         return true;
    //     }
    // }

}
