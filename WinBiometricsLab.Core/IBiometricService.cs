using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

public interface IBiometricService
{
    public void OpenSession();

    public void BeginEnroll(FingerPosition fingerPosition);

    public ICaptureEnrollResult CaptureEnroll();

    public IEnumerable<FingerPosition> GetEnrolledFingerPositions();

    public IBiometricIdentity CommitEnroll();

    public IVerifyResult Verify(FingerPosition fingerPosition);

    public void DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition);

    public IIdentifyResult Identify();

    public void CloseSession();
}
