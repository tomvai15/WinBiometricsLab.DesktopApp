using System.Security.Principal;
using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results;

public class BiometricIdentityAdapter(BiometricIdentity biometricIdentity): IBiometricIdentity
{
    public BiometricIdentity BiometricIdentity = biometricIdentity;

    public IdentityType Type => biometricIdentity.Type;

    public SecurityIdentifier Sid => biometricIdentity.Sid;

    public Guid TemplateGuid => biometricIdentity.TemplateGuid;
}
