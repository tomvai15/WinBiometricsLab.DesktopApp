using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Fakes;

public class FakeIdentifyResult : IIdentifyResult
{
    public FingerPosition FingerPosition { get; set; }

    public IBiometricIdentity Identity => new FakeBiometricIdentity();
    public RejectDetail RejectDetail { get; set; }
}
