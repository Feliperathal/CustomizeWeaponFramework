using HarmonyLib;
using UnityEngine;
using Verse;

// ReSharper disable InconsistentNaming

namespace CWF.HarmonyPatches;

[HarmonyPatch(typeof(Projectile), nameof(Projectile.Launch), typeof(Thing), typeof(Vector3), typeof(LocalTargetInfo),
    typeof(LocalTargetInfo), typeof(ProjectileHitFlags), typeof(bool), typeof(Thing), typeof(ThingDef))]
public static class Prefix_Projectile_Launch {
    public static void Prefix(Projectile __instance, Thing? equipment) {
        if (equipment == null || !equipment.TryGetComp<CompDynamicTraits>(out var compDynamicTraits)) return;

        foreach (var trait in compDynamicTraits.Traits) {
            if (trait.damageDefOverride != null) {
                __instance.damageDefOverride = trait.damageDefOverride;
            }

            if (trait.extraDamages.NullOrEmpty()) continue;

            __instance.extraDamages ??= [];
            __instance.extraDamages.AddRange(trait.extraDamages);
        }
    }
}