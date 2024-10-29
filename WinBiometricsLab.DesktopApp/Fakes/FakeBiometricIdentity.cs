using System.Security.Principal;
using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Fakes;

public record FakeBiometricIdentity : IBiometricIdentity
{
    private Random random = new();

    public IdentityType Type => IdentityType.Sid;

    public SecurityIdentifier Sid => new SecurityIdentifier(random.Next(10));

    public Guid TemplateGuid => Guid.Empty;
}
