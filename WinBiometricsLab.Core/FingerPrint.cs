using WinBiometricDotNet;

namespace WinBiometricsLab.Core;

public class FingerPrint
{
    public string Name { get; set; }
    public FingerPosition Position { get; set; }
    public required BiometricIdentity Identity { get; set; }
    public FunctionType AssignedFunction { get; set; }
}
