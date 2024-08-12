namespace Caisse_Leoni_gm7.Views;

public partial class LoadingPage : ContentPage
{
	public LoadingPage()
	{
		InitializeComponent();
        Initialize();

    }

    private async void Initialize()
    {
        await App.InitializeDatabaseAsync();
        await NavigateToMainPage();
    }

    private async Task NavigateToMainPage() 
    {
        await Navigation.PushAsync(new MainPage());
        // Remove the LoadingPage from the navigation stack
        Navigation.RemovePage(this);
    }
}