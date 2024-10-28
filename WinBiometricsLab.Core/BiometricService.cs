using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.Core;

public class BiometricService : IBiometricService
{
    private Session _session;
    private uint _unitId;

    public void OpenSession()
    {
        _session = WinBiometric.OpenSession();
        _unitId = WinBiometric.LocateSensor(_session);
    }

    public void BeginEnroll(FingerPosition fingerPosition)
    {
        WinBiometric.BeginEnroll(_session, fingerPosition, _unitId);
    }

    public ICaptureEnrollResult CaptureEnroll()
    {
        return new CaptureEnrollResultAdapter(WinBiometric.CaptureEnroll(_session));
    }

    public IEnumerable<FingerPosition> GetEnrolledFingerPositions()
    {
        return WinBiometric.EnumEnrollments(_session, _unitId);
    }

    public IBiometricIdentity CommitEnroll()
    {
        return new BiometricIdentityAdapter(WinBiometric.CommitEnroll(_session));
    }

    public IVerifyResult Verify(FingerPosition fingerPosition)
    {
        return new VerifyResultAdapter(WinBiometric.Verify(_session, fingerPosition));
    }

    public void DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition)
    {
        var adapter = (BiometricIdentityAdapter)identity;
        WinBiometric.DeleteTemplate(_session, _unitId, adapter.BiometricIdentity, fingerPosition);
    }

    public IIdentifyResult Identify()
    {
        return new IdentifyResultAdapter(WinBiometric.Identify(_session));
    }

    public void CloseSession()
    {
        WinBiometric.CloseSession(_session);
    }
}