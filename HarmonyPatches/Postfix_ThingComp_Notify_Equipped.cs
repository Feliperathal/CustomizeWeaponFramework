using HarmonyLib;
using Verse;

// ReSharper disable InconsistentNaming

namespace CWF.HarmonyPatches;

[HarmonyPatch(typeof(ThingComp), nameof(ThingComp.Notify_Equipped))]
public static class Postfix_ThingComp_Notify_Equipped {
    public static void Postfix(ThingComp __instance, Pawn pawn) {
        var compAbilityProvider = __instance.parent.GetComp<CompAbilityProvider>();
        compAbilityProvider?.OnEquipped(pawn);
    }
}