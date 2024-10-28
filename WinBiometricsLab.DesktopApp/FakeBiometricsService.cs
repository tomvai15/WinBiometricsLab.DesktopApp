using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp;

public class FakeBiometricsService : IBiometricService
{
    public void BeginEnroll(FingerPosition fingerPosition)
    {
        Console.WriteLine(nameof(BeginEnroll));
    }

    public ICaptureEnrollResult CaptureEnroll()
    {
        return new FakeCaptureEnrollResult();
    }

    public IBiometricIdentity CommitEnroll()
    {
        return new FakeBiometricIdentity();
    }

    public void CloseSession()
    {
        Console.WriteLine(nameof(CloseSession));
    }

    public void DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition)
    {
        Console.WriteLine(nameof(DeleteTemplate));
    }

    public IEnumerable<FingerPosition> GetEnrolledFingerPositions()
    {
        return [];
    }

    public IIdentifyResult Identify()
    {
        return new FakeIdentifyResult();
    }

    public void OpenSession()
    {
        
    }

    public IVerifyResult Verify(FingerPosition fingerPosition)
    {
        return new FakeVerifyResult();
    }
}
