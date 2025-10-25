using Verse;
using CWF.Extensions;
using JetBrains.Annotations;

namespace CWF;

[UsedImplicitly]
public class WeaponMatcher {
    [UsedImplicitly]
    public List<ThingDef> weaponDefs = [];

    [UsedImplicitly]
    public List<string> weaponTags = [];

    public bool IsMatch(ThingDef weaponDef) {
        if (weaponDefs.Contains(weaponDef)) return true;

        if (weaponTags.Empty() || weaponDef.weaponTags.NullOrEmpty()) return false;

        return weaponTags.Any(tag => weaponDef.weaponTags.Contains(tag));
    }
}