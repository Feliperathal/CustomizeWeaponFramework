namespace CWF;

public class CompRenamable : ThingComp {
    private string? _nickname;

    public string? Nickname {
        get => _nickname;
        set {
            _nickname = value.NullOrEmpty() ? null : value;

            if (_nickname != null && parent.TryGetComp<CompArt>(out var compArt)) {
                compArt.Title = _nickname;
            }
        }
    }

    public override string TransformLabel(string label) => Nickname ?? label;

    public override void PostExposeData() {
        base.PostExposeData();
        Scribe_Values.Look(ref _nickname, "nickname");
    }
}