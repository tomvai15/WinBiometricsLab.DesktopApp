using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results
{
    public class CaptureEnrollResultAdapter(CaptureEnrollResult captureEnrollResult) : ICaptureEnrollResult
    {
        public int OperationStatus => captureEnrollResult.OperationStatus;
        public RejectDetail RejectDetail => captureEnrollResult.RejectDetail;
        public bool IsRequiredMoreData => captureEnrollResult.IsRequiredMoreData;
    }
}
