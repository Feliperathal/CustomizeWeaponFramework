using RimWorld;
using Verse;

namespace CWF;

public class ModificationData : IExposable {
    public ModificationType Type;
    public Part Part;
    public WeaponTraitDef Trait = null!;
    public ThingDef ModuleDef = null!;

    public void ExposeData() {
        Scribe_Values.Look(ref Type, "type");
        Scribe_Values.Look(ref Part, "part");
        Scribe_Defs.Look(ref Trait, "trait");
        Scribe_Defs.Look(ref ModuleDef, "moduleDef");
    }
}

public enum ModificationType {
    Install,
    Uninstall
}