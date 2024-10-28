using System.Collections.ObjectModel;
using WinBiometricsLab.DesktopApp.ViewModels;

namespace WinBiometricsLab.DesktopApp.Views;

public partial class AddFingerprintPage : ContentPage
{
	public AddFingerprintPage(IBiometricService biometricService, ObservableCollection<Fingerprint> fingerprints, Action<Page> callback)
	{
		InitializeComponent();

        BindingContext = new AddFingerprintPageViewModel(biometricService, fingerprints, () => callback(this));
    }
}