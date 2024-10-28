using WinBiometricsLab.Core;

public static class FunctionFactory
{
    private static Action GetFunction(FunctionType functionType)
    {
        return () => PerformFunction(functionType);
    }

    private static void PerformFunction(FunctionType functionType)
    {
        Console.WriteLine($"Perfomed {nameof(functionType)} function");
    }
}
