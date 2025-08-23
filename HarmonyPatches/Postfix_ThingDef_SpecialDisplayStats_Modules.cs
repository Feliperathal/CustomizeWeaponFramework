using System.Text;
using HarmonyLib;
using RimWorld;
using Verse;

namespace CWF.HarmonyPatches;

[HarmonyPatch(typeof(ThingDef), nameof(ThingDef.SpecialDisplayStats))]
public static class Postfix_ThingDef_SpecialDisplayStats_Modules {
    public static IEnumerable<StatDrawEntry> Postfix(IEnumerable<StatDrawEntry> __result, ThingDef __instance) {
        foreach (var entry in __result) {
            yield return entry;
        }

        var ext = __instance.GetModExtension<TraitModuleExtension>();
        if (ext?.weaponTraitDef == null) yield break;

        var traitDef = ext.weaponTraitDef;
        var part = ext.part;

        var sb = new StringBuilder();
        var effectLines = TraitModuleDatabase.GetTraitEffectLines(traitDef);

        if (effectLines.Count > 0) {
            sb.AppendLine($"CWF_UI_ModuleEffectsDesc".Translate(traitDef.Named("MODULE")) + ":");
            sb.AppendLine();
            sb.AppendLine(string.Join("\n", effectLines));
            sb.AppendLine();
        }

        sb.AppendLine("CWF_UI_PartOf".Translate() + ": " + $"CWF_UI_{part}".Translate());

        yield return new StatDrawEntry(
            StatCategoryDefOf.BasicsImportant,
            "CWF_UI_ModuleEffects".Translate(),
            traitDef.LabelCap,
            sb.ToString().TrimEndNewlines(),
            1000
        );
    }
}