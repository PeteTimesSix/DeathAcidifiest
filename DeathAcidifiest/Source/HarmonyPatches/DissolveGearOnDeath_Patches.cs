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
                Rand.PushState(__instance.Pawn.thingIDNumber ^ 6457422); //made that number up
                var min = Math.Max(1, corpse.MaxHitPoints / 50);
                var max = Math.Max(1, corpse.MaxHitPoints / 5);
                corpse.HitPoints = Rand.RangeInclusiveSeeded(min, max, __instance.Pawn.thingIDNumber ^ 6457422);
                Rand.PopState();
            }
        }
    }

    [HarmonyPatch(typeof(HediffComp_DissolveGearOnDeath), nameof(HediffComp_DissolveGearOnDeath.Notify_PawnKilled))]
    public static class HediffComp_DissolveGearOnDeath_Notify_PawnKilled_Patches
    {
        [HarmonyPostfix]
        public static void Postfix(HediffComp_DissolveGearOnDeath __instance)
        {
            var pawn = __instance?.Pawn;
            if(pawn != null)
            {
                pawn.inventory.DestroyAll(DestroyMode.Vanish);
            }
            var injuryDef = __instance.Props.injuryCreatedOnDeath;
            var bodyPart = __instance.parent.Part;
            if(injuryDef != null && bodyPart != null && !pawn.health.hediffSet.hediffs.Any(h => h.Part == bodyPart && h.def == injuryDef))
                pawn.health.AddHediff(injuryDef, bodyPart);
        }
    }
}
