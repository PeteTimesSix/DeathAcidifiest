using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace PeteTimesSix.DeathAcidifiest.HarmonyPatches
{
    [HarmonyPatch(typeof(HediffComp_DissolveGearOnDeath),nameof(HediffComp_DissolveGearOnDeath.Notify_PawnDied))]
    public static class HediffComp_DissolveGearOnDeath_Notify_PawnDied_Patches
    {
        [HarmonyPostfix]
        public static void Postfix(HediffComp_DissolveGearOnDeath __instance)
        {
            var corpse = __instance?.Pawn?.Corpse;
            if(corpse != null) 
            {
                CompRottable compRottable = corpse.TryGetComp<CompRottable>();
                if(compRottable != null) 
                {
                    compRottable.RotProgress = compRottable.PropsRot.TicksToDessicated;
                }
            }
        }
    }
}
