using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results;

public interface IVerifyResult
{
    bool IsMatch { get; }
    int OperationStatus { get; }
    RejectDetail RejectDetail { get; }
}
