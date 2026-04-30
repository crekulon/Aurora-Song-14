namespace Content.Server._AS.Humanoid;

/// <summary>
/// Sets a point light on a humanoid to their eye color
/// Intended for use with things that already have a PointLight like fairies
/// </summary>
[RegisterComponent, Access(typeof(EyeColorPointLightSystem))]
public sealed partial class EyeColorPointLightComponent : Component
{
}