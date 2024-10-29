using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

public interface IBiometricService
{
    public Task OpenSession();

    public Task BeginEnroll(FingerPosition fingerPosition);

    public Task<ICaptureEnrollResult> CaptureEnroll();

    public Task<IEnumerable<FingerPosition>> GetEnrolledFingerPositions();

    public Task<IBiometricIdentity> CommitEnroll();

    public Task<IVerifyResult> Verify(FingerPosition fingerPosition);

    public Task DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition);

    public Task<IIdentifyResult> Identify();

    public Task CloseSession();
}
