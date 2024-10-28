using WinBiometricDotNet;

namespace WinBiometricsLab.Core.Results;

public class VerifyResultAdapter(VerifyResult verifyResult): IVerifyResult
{
    public bool IsMatch => verifyResult.IsMatch;
    public int OperationStatus => verifyResult.OperationStatus;
    public RejectDetail RejectDetail => verifyResult.RejectDetail;
}
