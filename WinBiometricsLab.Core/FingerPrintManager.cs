//using Biometrics;
//using WinBiometricDotNet;

//public class FingerPrintManager
//{
//    private readonly List<FingerPrint> _fingerPrints = new List<FingerPrint>();

//    public IReadOnlyList<FingerPrint> FingerPrints => _fingerPrints.AsReadOnly();

//    public bool HasFingerPrints => _fingerPrints.Any();

//    public void AddFingerPrint(FingerPrint fingerPrint)
//    {
//        _fingerPrints.Add(fingerPrint);
//    }

//    public void RemoveFingerPrint(FingerPrint fingerPrint)
//    {
//        _fingerPrints.Remove(fingerPrint);
//    }

//    public FingerPrint GetFingerPrintById(string id)
//    {
//        return _fingerPrints.First(fp => fp.Id == id);
//    }

//    public FingerPrint GetFingerPrintByPosition(FingerPosition position)
//    {
//        return _fingerPrints.First(fp => fp.Position == position);
//    }
//}
