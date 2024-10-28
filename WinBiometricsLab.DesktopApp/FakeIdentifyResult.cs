using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp;

public class FakeIdentifyResult : IIdentifyResult
{
    public FingerPosition FingerPosition => FingerPosition.LeftIndex;

    public IBiometricIdentity Identity => new FakeBiometricIdentity();
    public RejectDetail RejectDetail => default;
}
