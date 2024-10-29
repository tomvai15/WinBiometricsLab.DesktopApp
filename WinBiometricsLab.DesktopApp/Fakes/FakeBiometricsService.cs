using System;
using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Fakes;

public class FakeBiometricsService : IBiometricService
{
    private readonly Random _random = new();

    public void BeginEnroll(FingerPosition fingerPosition)
    {
        Console.WriteLine(nameof(BeginEnroll));
    }

    public ICaptureEnrollResult CaptureEnroll()
    {
        Thread.Sleep(2000);
        return new FakeCaptureEnrollResult
        {
            IsRequiredMoreData = RandomBool,
            RejectDetail = RandomBool ? RejectDetail.FingerprintTooHigh : default,
        };
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
        return [FingerPosition.LeftIndex, FingerPosition.RightIndex];
    }

    public IIdentifyResult Identify()
    {
        Thread.Sleep(2000);
        return new FakeIdentifyResult
        {
            FingerPosition = FingerPosition.LeftIndex,
            RejectDetail = RandomBool ? RejectDetail.FingerprintTooHigh : default
        };
    }

    public void OpenSession()
    {
        Thread.Sleep(2000);
        Console.WriteLine(nameof(OpenSession));
    }

    public IVerifyResult Verify(FingerPosition fingerPosition)
    {
        Thread.Sleep(2000);
        return new FakeVerifyResult
        {
            IsMatch = RandomBool
        };
    }

    private bool RandomBool => _random.Next(0, 2) == 1;
}
