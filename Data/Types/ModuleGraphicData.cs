using JetBrains.Annotations;
using UnityEngine;
using Verse;

namespace CWF;

[UsedImplicitly]
public class ModuleGraphicData {
    [NoTranslate]
    [UsedImplicitly]
    public string? texturePath;

    [NoTranslate]
    [UsedImplicitly]
    public string? outlinePath;

    [UsedImplicitly]
    public Vector2? offset;

    [UsedImplicitly]
    public float? scale;
}