using Verse.AI;

namespace CWF;

public class ReloadAbilityJobSource : IExposable, ILoadReferenceable {
    private int _loadId = -1;

    public AbilityDef AbilityDef = null!;

    internal static Job Create(ReloadableAbility reloadable, List<Thing> resources, bool playerForced) {
        var job = JobMaker.MakeJob(DefDatabase<JobDef>.GetNamed("CWF_ReloadAbility"), reloadable.ReloadableThing);
        job.targetQueueB = [.. resources.Select(thing => new LocalTargetInfo(thing))];
        job.count = Math.Min(resources.Sum(thing => thing.stackCount),
            reloadable.MaxAmmoNeeded(allowForcedReload: true));
        job.source = new ReloadAbilityJobSource { AbilityDef = reloadable.AbilityDef };
        job.playerForced = playerForced;
        return job;
    }

    public void ExposeData() {
        Scribe_Defs.Look(ref AbilityDef, "abilityDef");
    }

    public string GetUniqueLoadID() {
        if (_loadId < 0) {
            _loadId = Find.UniqueIDsManager.GetNextThingID();
        }

        return $"CWF_ReloadAbilityJobSource_{_loadId}";
    }
}