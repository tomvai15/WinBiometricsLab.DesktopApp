using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results;

public class IdentifyResultAdapter(IdentifyResult identifyResult) : IIdentifyResult
{
    public FingerPosition FingerPosition => identifyResult.FingerPosition;
    public IBiometricIdentity Identity => new BiometricIdentityAdapter(identifyResult.Identity);
    public RejectDetail RejectDetail => identifyResult.RejectDetail;
}
