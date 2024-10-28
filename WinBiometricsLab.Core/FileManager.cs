//namespace WinBiometricsLab.Core;

//public class FileManager
//{
//    private const string FingerPrintsFileName = "fingerprints.json";

//    public void SaveFingerPrintsToFile(List<FingerPrint> fingerPrints)
//    {
//        try
//        {
//            var json = JsonConvert.SerializeObject(fingerPrints);
//            File.WriteAllText(FingerPrintsFileName, json);
//            AnsiConsole.MarkupLine(
//                "[green]Fingerprints saved successfully to fingerprints.json[/]"
//            );
//        }
//        catch (Exception ex)
//        {
//            AnsiConsole.MarkupLine($"[red]Failed to save fingerprints: {ex.Message}[/]");
//        }
//    }

//    public List<FingerPrint> LoadFingerPrintsFromFile()
//    {
//        if (!File.Exists(FingerPrintsFileName))
//        {
//            AnsiConsole.MarkupLine("[yellow]No saved fingerprints found.[/]");
//            return new List<FingerPrint>();
//        }

//        try
//        {
//            var json = File.ReadAllText(FingerPrintsFileName);
//            var loadedFingerPrints =
//                JsonConvert.DeserializeObject<List<FingerPrint>>(json) ?? new List<FingerPrint>();
//            AnsiConsole.MarkupLine("[green]Fingerprints loaded successfully from file.[/]");

//            foreach (var fingerPrint in loadedFingerPrints)
//            {
//                fingerPrint.AssignedFunction = FunctionFactory.GetFunctionByName(
//                    fingerPrint.AssignedFunctionName
//                );
//            }

//            return loadedFingerPrints;
//        }
//        catch (Exception ex)
//        {
//            AnsiConsole.MarkupLine($"[red]Failed to load fingerprints[/]:");
//            AnsiConsole.WriteLine(ex.Message);
//            return new List<FingerPrint>();
//        }
//    }
//}
