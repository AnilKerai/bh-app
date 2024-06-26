using Auth0.OidcClient;

namespace BuildHub.MAUI;

public partial class App : Application
{
	public App(Auth0Client client)
	{
		InitializeComponent();

		MainPage = new MainPage(client);
	}
}
