using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinBiometricDotNet;
using WinBiometricsLab.Core;

namespace WinBiometricsLab.DesktopApp.ViewModels
{
    public class FingerprintViewModel : INotifyPropertyChanged
    {
        private readonly IBiometricService _biometricService;

        public List<string> FunctionTypes { get; set; } = Enum.GetNames(typeof(FunctionType)).ToList();


        private string _selectedFunction = FunctionType.a.ToString();
        public string SelectedFunction
        {
            get => _selectedFunction;
            set
            {
                _selectedFunction = value;
                OnPropertyChanged();
            }
        }

        public List<string> FingerprintPostitions { get; set; } = Enum.GetNames(typeof(FingerPosition))
            .Where(x => !x.StartsWith("Un")).ToList();


        private string _selectedFingerprintPostition = FingerPosition.LeftIndex.ToString();
        public string SelectedFingerprintPostition
        {
            get => _selectedFingerprintPostition;
            set
            {
                _selectedFingerprintPostition = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Fingerprint> Fingerprints { get; set; }

        private Fingerprint _fingerprint;
        public Fingerprint SelectedFingerprint
        {
            get => _fingerprint; set
            {
                _fingerprint = value;
                if (value != null)
                {
                    NameInput = value.Name;
                    SelectedFunction = value.AssignedFunction.ToString();
                }
                OnPropertyChanged();
            }
        }

        public bool IsSelected => SelectedFingerprint != null;

        private string _nameInput;

        public string NameInput
        {
            get => _nameInput;
            set
            {
                _nameInput = value;
                OnPropertyChanged();
            }
        }

        private string _infoText = "Infoo";

        public string InfoText
        {
            get => _infoText;
            set
            {
                _infoText = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddFingerprintCommand { get; }
        public ICommand UpdateFingerprintCommand { get; }
        public ICommand DeleteFingerprintCommand { get; }
        public ICommand IdentifyFingerprintCommand { get; }
        public ICommand VerifyFingerprintCommand { get; }

        public FingerprintViewModel(IBiometricService biometricService)
        {
            _biometricService = biometricService;
            Fingerprints = new ObservableCollection<Fingerprint>();

            AddFingerprintCommand = new Command(AddFingerprint);
            UpdateFingerprintCommand = new Command(UpdateFingerprint);
            DeleteFingerprintCommand = new Command(DeleteFingerprint);
            IdentifyFingerprintCommand = new Command(IdentifyFingerprint);
            VerifyFingerprintCommand = new Command(VerifyFingerprint);
        }

        public void AddFingerprint()
        {
            var position = Enum.Parse<FingerPosition>(SelectedFingerprintPostition);
            _biometricService.BeginEnroll(position);

            DisplayInfo("Scan finger");
            var result = _biometricService.CaptureEnroll();
            while (result.IsRequiredMoreData || result.RejectDetail != default)
            {
                DisplayInfo("Scan finger again");
                result = _biometricService.CaptureEnroll();
            }

            var commitResult = _biometricService.CommitEnroll();
            DisplayInfo("Scan complete");

            var fingerprint = new Fingerprint
            {
                Name = position.ToString(),
                AssignedFunction = FunctionType.a,
                Position = position,
                Identity = commitResult
            };

            Fingerprints.Add(fingerprint);
            NameInput = string.Empty;
        }


        public void UpdateFingerprint()
        {
            _biometricService.DeleteTemplate(SelectedFingerprint.Identity, SelectedFingerprint.Position);

            if (SelectedFingerprint != null && !string.IsNullOrWhiteSpace(NameInput))
            {
                SelectedFingerprint.Name = NameInput;
                SelectedFingerprint.AssignedFunction = Enum.Parse<FunctionType>(SelectedFunction);
                NameInput = string.Empty;

                OnPropertyChanged(nameof(Fingerprints)); // Refresh the CollectionView
            }
        }

        public void DeleteFingerprint()
        {
            if (SelectedFingerprint != null)
            {
                Fingerprints.Remove(SelectedFingerprint);
                SelectedFingerprint = null;
            }
        }

        public void IdentifyFingerprint()
        {
            var result = _biometricService.Identify();

            if (result.RejectDetail != default)
            {
                DisplayInfo("Fingerprint not recognized");
                return;
            }
            var identifiedFingerprint = Fingerprints.FirstOrDefault(x => x.Position == result.FingerPosition);
            if (identifiedFingerprint == null)
            {
                DisplayInfo($"Fingerprint {result.FingerPosition} recognized");
                return;
            }

            DisplayInfo($"Fingerprint {result.FingerPosition} recognized. Performing function {identifiedFingerprint.AssignedFunction}");
        }

        public void VerifyFingerprint()
        {
            var position = Enum.Parse<FingerPosition>(SelectedFingerprintPostition);
            var result = _biometricService.Verify(position);

            if (!result.IsMatch)
            {
                DisplayInfo($"Fingerprint does not match {position}");
                return;
            }

            DisplayInfo($"Fingerprint matches");
        }

        private void DisplayInfo(string text)
        {
            InfoText = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
