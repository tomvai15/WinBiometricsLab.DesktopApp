using System.Security.Principal;
using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results;

public interface IBiometricIdentity
{
    public IdentityType Type { get; }
    public SecurityIdentifier Sid { get; }

    public Guid TemplateGuid { get; }
}
