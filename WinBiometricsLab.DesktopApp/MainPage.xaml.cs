using WinBiometricsLab.DesktopApp.ViewModels;
using WinBiometricsLab.DesktopApp.Views;

namespace WinBiometricsLab.DesktopApp
{
    public partial class MainPage : ContentPage
    {
        private IBiometricService service = new FakeBiometricsService();

        public MainPage()
        {
            InitializeComponent();
            var service = new FakeBiometricsService();

            BindingContext = new FingerprintViewModel(service);
        }

        private async void OnInitiateAddFingerprint(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddFingerprintPage(service, ((FingerprintViewModel)BindingContext).Fingerprints));
        }
    }
}
