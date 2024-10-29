using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.Core;

public class BiometricService : IBiometricService
{
    private Session _session;
    private uint _unitId;

    public Task OpenSession()
    {
        _session = WinBiometric.OpenSession();
        _unitId = WinBiometric.LocateSensor(_session);
        return Task.CompletedTask;
    }

    public Task BeginEnroll(FingerPosition fingerPosition)
    {
        WinBiometric.BeginEnroll(_session, fingerPosition, _unitId);
        return Task.CompletedTask;
    }

    public async Task<ICaptureEnrollResult> CaptureEnroll()
    {
        return new CaptureEnrollResultAdapter(WinBiometric.CaptureEnroll(_session));
    }

    public async Task<IEnumerable<FingerPosition>> GetEnrolledFingerPositions()
    {
        return WinBiometric.EnumEnrollments(_session, _unitId);
    }

    public async Task<IBiometricIdentity> CommitEnroll()
    {
        return new BiometricIdentityAdapter(WinBiometric.CommitEnroll(_session));
    }

    public async Task<IVerifyResult> Verify(FingerPosition fingerPosition)
    {
        return new VerifyResultAdapter(WinBiometric.Verify(_session, fingerPosition));
    }

    public Task DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition)
    {
        var adapter = (BiometricIdentityAdapter)identity;
        WinBiometric.DeleteTemplate(_session, _unitId, adapter.BiometricIdentity, fingerPosition);
        return Task.CompletedTask;
    }

    public async Task<IIdentifyResult> Identify()
    {
        return new IdentifyResultAdapter(WinBiometric.Identify(_session));
    }

    public Task CloseSession()
    {
        WinBiometric.CloseSession(_session);
        return Task.CompletedTask;
    }
}