using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Fakes;

public class FakeCaptureEnrollResult : ICaptureEnrollResult
{
    public int OperationStatus { get; set; }
    public RejectDetail RejectDetail { get; set; }
    public bool IsRequiredMoreData { get; set; }
}
