using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Fakes;

public class FakeVerifyResult : IVerifyResult
{
    public bool IsMatch { get; set; }

    public int OperationStatus { get; set; }

    public RejectDetail RejectDetail { get; set; }
}
