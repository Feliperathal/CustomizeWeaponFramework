using JetBrains.Annotations;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace CWF.HarmonyPatches;

// ReSharper disable once InconsistentNaming
[HarmonyPatch(typeof(Verb), nameof(Verb.BurstShotCount), MethodType.Getter)]
public static class Transpiler_Verb_BurstShotCount {
    [UsedImplicitly]
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
        var codes = new List<CodeInstruction>(instructions);
        var ceilToInt = AccessTools.Method(typeof(Mathf), nameof(Mathf.CeilToInt), [typeof(float)]);

        for (var i = 0; i < codes.Count - 2; i++) {
            if (codes[i].opcode != OpCodes.Ldarg_0 ||
                codes[i + 1].opcode != OpCodes.Ldloc_0 ||
                !codes[i + 2].Calls(ceilToInt)) continue;

            var injected = new List<CodeInstruction> {
                new(OpCodes.Ldloc_0),
                new(OpCodes.Ldarg_0),
                CodeInstruction.Call(typeof(Transpiler_Verb_BurstShotCount), nameof(ApplyDynamicMultipliers)),
                new(OpCodes.Stloc_0)
            };
            injected[0].labels.AddRange(codes[i].labels);
            codes[i].labels.Clear();
            codes.InsertRange(i, injected);
            return codes;
        }

        Log.Error("[CWF] Verb.get_BurstShotCount transpiler failed.");
        return codes;
    }

    private static float ApplyDynamicMultipliers(float original, Verb verb) {
        var equipment = verb.EquipmentSource;
        if (equipment == null || !equipment.TryGetComp<CompDynamicTraits>(out var dynamicTraits)) return original;

        return dynamicTraits.Traits
            .Aggregate(original, (current, trait) => current * trait.burstShotCountMultiplier);
    }
}