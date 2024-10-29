using WinBiometricsLab.DesktopApp.ViewModels;

namespace WinBiometricsLab.DesktopApp.Views;

public partial class FingerprintPage : ContentPage
{
    private readonly IBiometricService _biometricService; 

    public FingerprintPage(IBiometricService biometricService)
	{
		InitializeComponent();

        _biometricService = biometricService;
        BindingContext = new FingerprintViewModel(_biometricService);

    }

    private async void OnInitiateAddFingerprint(object sender, EventArgs e)
    {
        var callback = Navigation.RemovePage;
        var page = new AddFingerprintPage(_biometricService, ((FingerprintViewModel)BindingContext).Fingerprints, callback);
        await Navigation.PushAsync(page);
    }

    private async void OnEndSession(object sender, EventArgs e)
    {
        ((FingerprintViewModel)BindingContext).EndSession();
        Navigation.RemovePage(this);
    }
}