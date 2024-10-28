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
            var callback = (Page page) => Navigation.RemovePage(page); ;
            var page = new AddFingerprintPage(service, ((FingerprintViewModel)BindingContext).Fingerprints, callback);
            await Navigation.PushAsync(page);
        }
    }
}
