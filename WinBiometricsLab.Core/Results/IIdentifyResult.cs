using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results;

public interface IIdentifyResult
{
    FingerPosition FingerPosition { get; }
    IBiometricIdentity Identity { get; }
    RejectDetail RejectDetail { get; }
}
