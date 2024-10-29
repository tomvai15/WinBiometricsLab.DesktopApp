using WinBiometricsLab.Core;
using WinBiometricsLab.DesktopApp.Fakes;
using WinBiometricsLab.DesktopApp.ViewModels;
using WinBiometricsLab.DesktopApp.Views;

namespace WinBiometricsLab.DesktopApp
{
    public partial class MainPage : ContentPage
    {
        private readonly IBiometricService _biometricService = AppSettings.UseFakeBiometrics 
            ? new FakeBiometricsService() 
            : new BiometricService();

        public MainPage()
        {
            InitializeComponent();
            var service = new FakeBiometricsService();

            BindingContext = new FingerprintViewModel(service);
        }

        private async void StartProgram(object sender, EventArgs e)
        {
            InfoText.Text = "Please touch scanner";

            await Task.Run(() => _biometricService.OpenSession());
            var page = new FingerprintPage(_biometricService);
            await Navigation.PushAsync(page);
        }
    }
}
