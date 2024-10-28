using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp;

public class FakeVerifyResult : IVerifyResult
{
    public bool IsMatch => true;

    public int OperationStatus => 0;

    public RejectDetail RejectDetail => default;
}
