using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinBiometricDotNet;
using WinBiometricsLab.Core;

namespace WinBiometricsLab.DesktopApp.ViewModels
{
    public class AddFingerprintPageViewModel : INotifyPropertyChanged
    {
        private readonly IBiometricService _biometricService;
        private readonly Action _callback;

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

        public AddFingerprintPageViewModel(IBiometricService biometricService, 
            ObservableCollection<Fingerprint> fingerprints, 
            Action callback)
        {
            _biometricService = biometricService;
            Fingerprints = fingerprints;
            _callback = callback;

            AddFingerprintCommand = new Command(AddFingerprint);
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

            _callback();
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
