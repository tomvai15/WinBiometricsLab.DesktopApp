using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WinBiometricDotNet;
using WinBiometricsLab.Core;
using WinBiometricsLab.Core.Results;
using WinBiometricsLab.DesktopApp.Models;

namespace WinBiometricsLab.DesktopApp.ViewModels
{
    public class AddFingerprintPageViewModel : INotifyPropertyChanged
    {
        private readonly IBiometricService _biometricService;
        private readonly Action _callback;

        public AddFingerprintPageViewModel(IBiometricService biometricService,
            ObservableCollection<Fingerprint> fingerprints,
            Action callback)
        {
            _biometricService = biometricService;
            Fingerprints = fingerprints;
            _callback = callback;

            AddFingerprintCommand = new Command(AddFingerprint);

            var existingPositions = Fingerprints.Select(x => x.Position).ToHashSet();

            FingerprintPostitions = Enum.GetValues(typeof(FingerPosition))
                .Cast<FingerPosition>()
                .Where(p => !existingPositions.Contains(p))
                .Select(x => x.ToString())
                .Where(x => !x.StartsWith("Un")).ToList();
        }


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

        public List<string> FingerprintPostitions { get; set; }


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

        private string _infoText = "...";

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


        public async void AddFingerprint()
        {
            var position = Enum.Parse<FingerPosition>(SelectedFingerprintPostition);
            _biometricService.BeginEnroll(position);


            ICaptureEnrollResult result = null;
            DisplayInfo("Scan finger");
            await Task.Run(() => result = _biometricService.CaptureEnroll());

            while (result.IsRequiredMoreData || result.RejectDetail != default)
            {
                DisplayInfo("Scan finger again");
                await Task.Run(() => result = _biometricService.CaptureEnroll());
                DisplayInfo("");
                await Task.Delay(100);
            }

            var commitResult = _biometricService.CommitEnroll();
            DisplayInfo("Scan complete");

            var fingerprint = new Fingerprint
            {
                Name = position.ToString(),
                AssignedFunction = FunctionType.a,
                Position = position
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
