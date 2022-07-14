using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using UnityEngine;
using HarmonyLib;
using System.Reflection;
using RimWorld;
using PeteTimesSix.DeathAcidifiest.HarmonyPatches;

namespace PeteTimesSix.DeathAcidifiest
{
    public class DeathAcidifiestMod : Mod
    {
        public DeathAcidifiestMod(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("PeteTimesSix.DeathAcidifiest");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
	}
}
