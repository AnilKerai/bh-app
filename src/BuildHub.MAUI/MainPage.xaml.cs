using Auth0.OidcClient;

namespace BuildHub.MAUI;

public partial class MainPage : ContentPage
{
	private readonly Auth0Client _client;

	public MainPage(Auth0Client client)
	{
		_client = client;
		InitializeComponent();
	}
	
	private async void OnLoginClicked(object sender, EventArgs e)
	{
		var loginResult = await _client.LoginAsync();

		if (!loginResult.IsError)
		{
			LoginView.IsVisible = false;
			HomeView.IsVisible = true;
		}
		else
		{
			await DisplayAlert("Error", loginResult.ErrorDescription, "OK");
		}
	}

	private async void OnLogoutClicked(object sender, EventArgs e)
	{
		await _client.LogoutAsync();

		LoginView.IsVisible = true;
		HomeView.IsVisible = false;
	}
}
