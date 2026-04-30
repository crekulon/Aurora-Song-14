using Content.Shared.GameTicking;
using Content.Shared.Humanoid;
using Robust.Server.GameObjects;
using Robust.Shared.Maths;

namespace Content.Server._AS.Humanoid;

public sealed class EyeColorPointLightSystem : EntitySystem
{
    [Dependency] private readonly PointLightSystem _lights = default!;

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<EyeColorPointLightComponent, ComponentStartup>(OnStartup);
        SubscribeLocalEvent<EyeColorPointLightComponent, PlayerSpawnCompleteEvent>(OnPlayerSpawnComplete);
    }

    private void OnStartup(EntityUid uid, EyeColorPointLightComponent component, ComponentStartup args)
    {
        ApplyEyeColorLight(uid);
    }

    private void OnPlayerSpawnComplete(EntityUid uid, EyeColorPointLightComponent component, ref PlayerSpawnCompleteEvent args)
    {
        ApplyEyeColorLight(uid);
    }

    private void ApplyEyeColorLight(EntityUid uid)
    {
        if (!TryComp(uid, out PointLightComponent? light))
            return;

        if (!TryComp(uid, out HumanoidAppearanceComponent? humanoid))
            return;

        var hsv = Color.ToHsv(humanoid.EyeColor);
        hsv.Z = 1f;
        var brightEyeColor = Color.FromHsv(hsv);

        _lights.SetColor(uid, brightEyeColor, light);
    }
}