using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp;

public  class FakeCaptureEnrollResult: ICaptureEnrollResult
{
    public int OperationStatus => 0;
    public RejectDetail RejectDetail => default;
    public bool IsRequiredMoreData => false;
}
