using System.Collections.Generic;
using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.Core;

public class BiometricService : IBiometricService
{
    private Session _session;
    private uint _unitId;

    public async Task OpenSession()
    {
        await Task.Run(() =>
        {
            _session = WinBiometric.OpenSession();
            _unitId = WinBiometric.LocateSensor(_session);
        });
    }

    public async Task BeginEnroll(FingerPosition fingerPosition)
    {
        await Task.Run(() => WinBiometric.BeginEnroll(_session, fingerPosition, _unitId));
    }

    public async Task<ICaptureEnrollResult> CaptureEnroll()
    {
        return await Task.Run(() => new CaptureEnrollResultAdapter(WinBiometric.CaptureEnroll(_session)));
    }

    public async Task<IEnumerable<FingerPosition>> GetEnrolledFingerPositions()
    {
        return await Task.Run(() => WinBiometric.EnumEnrollments(_session, _unitId));
    }

    public async Task<IBiometricIdentity> CommitEnroll()
    {
        return await Task.Run(() => new BiometricIdentityAdapter(WinBiometric.CommitEnroll(_session)));
    }

    public async Task<IVerifyResult> Verify(FingerPosition fingerPosition)
    {
        return await Task.Run(() => new VerifyResultAdapter(WinBiometric.Verify(_session, fingerPosition)));
    }

    public async Task DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition)
    {
        await Task.Run(() =>
        {
            var adapter = (BiometricIdentityAdapter)identity;
            WinBiometric.DeleteTemplate(_session, _unitId, adapter.BiometricIdentity, fingerPosition);
        });
    }

    public async Task<IIdentifyResult> Identify()
    {
        return await Task.Run(() => new IdentifyResultAdapter(WinBiometric.Identify(_session)));
    }

    public async Task CloseSession()
    {
        await Task.Run(() => WinBiometric.CloseSession(_session));
    }
}