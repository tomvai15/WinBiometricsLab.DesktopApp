using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results
{
    public interface ICaptureEnrollResult
    {
        public int OperationStatus { get; }
        public RejectDetail RejectDetail { get; }
        public bool IsRequiredMoreData { get; }
    }
}
