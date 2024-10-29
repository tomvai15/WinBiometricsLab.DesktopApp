using WinBiometricDotNet;
using WinBiometricsLab.Core.Results;

namespace WinBiometricsLab.DesktopApp.Fakes;

public class FakeBiometricsService : IBiometricService
{
    private readonly Random _random = new();

    public Task BeginEnroll(FingerPosition fingerPosition)
    {
        Console.WriteLine(nameof(BeginEnroll));
        return Task.CompletedTask;
    }

    public async Task<ICaptureEnrollResult> CaptureEnroll()
    {
        await Task.Delay(2000);
        return new FakeCaptureEnrollResult
        {
            IsRequiredMoreData = RandomBool,
            RejectDetail = RandomBool ? RejectDetail.FingerprintTooHigh : default,
        };
    }

    public async Task<IBiometricIdentity> CommitEnroll()
    {
        return new FakeBiometricIdentity();
    }

    public Task CloseSession()
    {
        Console.WriteLine(nameof(CloseSession));
        return Task.CompletedTask;
    }

    public Task DeleteTemplate(IBiometricIdentity identity, FingerPosition fingerPosition)
    {
        Console.WriteLine(nameof(DeleteTemplate));
        return Task.CompletedTask;
    }

    public async Task<IEnumerable<FingerPosition>> GetEnrolledFingerPositions()
    {
        return [FingerPosition.LeftIndex, FingerPosition.RightIndex];
    }

    public async Task<IIdentifyResult> Identify()
    {
        await Task.Delay(2000);
        return new FakeIdentifyResult
        {
            FingerPosition = FingerPosition.LeftIndex,
            RejectDetail = RandomBool ? RejectDetail.FingerprintTooHigh : default
        };
    }

    public async Task OpenSession()
    {
        await Task.Delay(2000);
        Console.WriteLine(nameof(OpenSession));
    }

    public async Task<IVerifyResult> Verify(FingerPosition fingerPosition)
    {
        await Task.Delay(2000);
        return new FakeVerifyResult
        {
            IsMatch = RandomBool
        };
    }

    private bool RandomBool => _random.Next(0, 2) == 1;
}
