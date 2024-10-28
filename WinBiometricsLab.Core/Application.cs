//using System.Text.Json;
//using Biometrics;
//using WinBiometricDotNet;

//public class Application
//{
//    private readonly BiometricService _biometricService;
//    private readonly FingerPrintManager _fingerPrintManager;
//    private readonly FileManager _fileManager;

//    private readonly IEnumerable<FingerPosition> _possibleFingerPositions = new[]
//    {
//        FingerPosition.LeftLittle,
//        FingerPosition.LeftRing,
//        FingerPosition.LeftMiddle,
//        FingerPosition.LeftIndex,
//        FingerPosition.LeftThumb,
//        FingerPosition.RightThumb,
//        FingerPosition.RightIndex,
//        FingerPosition.RightMiddle,
//        FingerPosition.RightRing,
//        FingerPosition.RightLittle
//    };

//    public Application()
//    {
//        _biometricService = new BiometricService();
//        _fingerPrintManager = new FingerPrintManager();
//        _fileManager = new FileManager();
//    }

//    public void Run()
//    {
//        AnsiConsole.MarkupLine("[green]Before starting, please use the biometric sensor.[/]");
//        _biometricService.OpenSession();

//        while (true)
//        {
//            DisplayFingerPrints();

//            var menuSelection = AnsiConsole.Prompt(
//                new SelectionPrompt<string>()
//                    .Title("Choose an [green]option[/]:")
//                    .PageSize(10)
//                    .AddChoices(
//                        "1 - Add Fingerprint",
//                        "2 - Manage Fingerprints",
//                        "3 - Execute Fingerprint Function",
//                        "4 - Save Fingerprints to File",
//                        "5 - Read Saved Fingerprints from File",
//                        "6 - Exit"
//                    )
//            );

//            switch (menuSelection)
//            {
//                case "1 - Add Fingerprint":
//                    AddFingerPrint();
//                    break;
//                case "2 - Manage Fingerprints":
//                    ManageFingerPrints();
//                    break;
//                case "3 - Execute Fingerprint Function":
//                    ExecuteFingerPrintFunction();
//                    break;
//                case "4 - Save Fingerprints to File":
//                    SaveFingerPrintsToFile();
//                    break;
//                case "5 - Read Saved Fingerprints from File":
//                    ShowFingerPrintsFromFile();
//                    break;
//                case "6 - Exit":
//                    Exit();
//                    return;
//                default:
//                    AnsiConsole.MarkupLine("[red]Invalid option![/]");
//                    break;
//            }

//            AnsiConsole.MarkupLine("\nPress any key to return to the menu...");
//            Console.ReadKey(true);
//            Console.Clear();
//        }
//    }

//    private void DisplayFingerPrints()
//    {
//        if (!_fingerPrintManager.HasFingerPrints)
//        {
//            AnsiConsole.MarkupLine("[yellow]No fingerprints enrolled yet.[/]");
//            return;
//        }

//        DisplayFingerPrints(_fingerPrintManager.FingerPrints);
//    }

//    private void DisplayFingerPrints(IEnumerable<FingerPrint> fingerPrints)
//    {
//        foreach (var fingerPrint in fingerPrints)
//        {
//            var fingerPrintIdentitySerialized = JsonSerializer.Serialize(fingerPrint.Identity);
//            var jsonText = new JsonText(fingerPrintIdentitySerialized);
//            AnsiConsole.Write(
//                new Panel(jsonText)
//                    .Header(
//                        $"{fingerPrint.Id} - {fingerPrint.Position} - {fingerPrint.AssignedFunction.Method.Name}"
//                    )
//                    .Collapse()
//                    .RoundedBorder()
//                    .BorderColor(Color.Yellow)
//            );
//        }
//    }

//    private void AddFingerPrint()
//    {
//        AnsiConsole.MarkupLine("[green]Starting new fingerprint addition process:[/]");
//        var fingerPrintId = AnsiConsole.Prompt(
//            new TextPrompt<string>("Enter the [green]ID for the new fingerprint[/]:").PromptStyle(
//                "red"
//            )
//        );

//        var availablePositions = _possibleFingerPositions.Except(
//            _fingerPrintManager.FingerPrints.Select(f => f.Position)
//        );

//        var fingerPosition = AnsiConsole.Prompt(
//            new SelectionPrompt<FingerPosition>()
//                .Title("Select the [green]finger[/] you will use:")
//                .AddChoices(availablePositions)
//        );

//        var functionToAssign = SelectFunctionToAssign();

//        AnsiConsole.MarkupLine($"Please scan your {fingerPosition} finger.");

//        _biometricService.BeginEnroll(fingerPosition);

//        var captureEnrollResult = _biometricService.CaptureEnroll();
//        while (
//            captureEnrollResult.IsRequiredMoreData || captureEnrollResult.RejectDetail != default
//        )
//        {
//            AnsiConsole.MarkupLine($"Please scan your {fingerPosition} again for more data.");
//            captureEnrollResult = _biometricService.CaptureEnroll();
//        }

//        var biometricIdentity = _biometricService.CommitEnroll();

//        var fingerPrint = new FingerPrint
//        {
//            Id = fingerPrintId,
//            Position = fingerPosition,
//            Identity = biometricIdentity,
//            AssignedFunction = functionToAssign,
//            AssignedFunctionName = functionToAssign.Method.Name
//        };

//        _fingerPrintManager.AddFingerPrint(fingerPrint);
//        AnsiConsole.MarkupLine($"[green]{fingerPosition} saved successfully.[/]");
//    }

//    private Action SelectFunctionToAssign()
//    {
//        var functionSelection = AnsiConsole.Prompt(
//            new SelectionPrompt<string>()
//                .Title("Choose a [green]function[/] to assign:")
//                .AddChoices(FunctionFactory.AvailableFunctions.Keys)
//        );

//        return FunctionFactory.AvailableFunctions[functionSelection];
//    }

//    private void ManageFingerPrints()
//    {
//        if (!_fingerPrintManager.HasFingerPrints)
//        {
//            AnsiConsole.MarkupLine("[yellow]No fingerprints to manage.[/]");
//            return;
//        }

//        var fingerPrintId = AnsiConsole.Prompt(
//            new SelectionPrompt<string>()
//                .Title("Choose a [green]fingerprint[/] to manage:")
//                .PageSize(10)
//                .AddChoices(_fingerPrintManager.FingerPrints.Select(f => f.Id))
//        );

//        var selectedFingerPrint = _fingerPrintManager.GetFingerPrintById(fingerPrintId);

//        var actionSelection = AnsiConsole.Prompt(
//            new SelectionPrompt<string>()
//                .Title("Choose an [green]action[/]:")
//                .AddChoices("1 - Assign Function", "2 - Verify", "3 - Delete")
//        );

//        switch (actionSelection)
//        {
//            case "1 - Assign Function":
//                var function = SelectFunctionToAssign();
//                selectedFingerPrint.AssignedFunction = function;
//                AnsiConsole.MarkupLine("[green]Function assigned successfully.[/]");
//                break;
//            case "2 - Verify":
//                AnsiConsole.MarkupLine(
//                    $"Please scan your {selectedFingerPrint.Position} finger to verify."
//                );
//                var result = _biometricService.Verify(selectedFingerPrint.Position);
//                if (result.IsMatch)
//                {
//                    AnsiConsole.MarkupLine("[green]Verification successful![/]");
//                }
//                else
//                {
//                    AnsiConsole.MarkupLine(
//                        $"[red]Verification unsuccessful.[/] Reject reason: {result.RejectDetail}"
//                    );
//                }
//                break;
//            case "3 - Delete":
//                AnsiConsole.MarkupLine("[red]Please confirm deletion by scanning your finger.[/]");
//                _biometricService.DeleteTemplate(
//                    selectedFingerPrint.Identity,
//                    selectedFingerPrint.Position
//                );
//                _fingerPrintManager.RemoveFingerPrint(selectedFingerPrint);
//                AnsiConsole.MarkupLine("[green]Fingerprint deleted successfully.[/]");
//                break;
//        }
//    }

//    private void ExecuteFingerPrintFunction()
//    {
//        AnsiConsole.MarkupLine("Scan a finger to execute its assigned function.");

//        IdentifyResult? identifyResult = null;
//        while (true)
//        {
//            try
//            {
//                identifyResult = _biometricService.Identify();
//                break;
//            }
//            catch (WinBiometricException ex)
//            {
//                AnsiConsole.MarkupLine($"[red]{ex.Message}[/] Try again!");
//            }
//        }

//        var identifiedFingerPrint = _fingerPrintManager.GetFingerPrintByPosition(
//            identifyResult.FingerPosition
//        );

//        if (identifiedFingerPrint != null)
//        {
//            identifiedFingerPrint.AssignedFunction.Invoke();
//        }
//        else
//        {
//            AnsiConsole.MarkupLine("[red]Fingerprint not recognized![/]");
//        }
//    }

//    private void SaveFingerPrintsToFile()
//    {
//        AnsiConsole.MarkupLine("[green]Saving fingerprints to file...[/]");
//        _fileManager.SaveFingerPrintsToFile(_fingerPrintManager.FingerPrints.ToList());
//    }

//    private void ShowFingerPrintsFromFile()
//    {
//        AnsiConsole.MarkupLine("[green]Loading fingerprints from file...[/]");
//        var loadedFingerPrints = _fileManager.LoadFingerPrintsFromFile();
//        DisplayFingerPrints(loadedFingerPrints);
//    }

//    private void Exit()
//    {
//        AnsiConsole.MarkupLine("[red]Initiating fingerprint removal process...[/]");

//        foreach (var fingerPrint in _fingerPrintManager.FingerPrints)
//        {
//            _biometricService.DeleteTemplate(fingerPrint.Identity, fingerPrint.Position);
//        }

//        AnsiConsole.MarkupLine(
//            "[green]All fingerprints removed. Exiting the application... Goodbye![/]"
//        );
//        _biometricService.CloseSession();
//    }
//}
