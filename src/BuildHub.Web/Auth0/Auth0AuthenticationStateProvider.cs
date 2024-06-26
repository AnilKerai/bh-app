using Auth0.OidcClient;

namespace BuildHub.Web.Auth0;

using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

public class Auth0AuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());
    private readonly Auth0Client _client;

    public Auth0AuthenticationStateProvider(Auth0Client client) 
    {
        _client = client;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(_currentUser));

    public Task LogInAsync()
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithAuth0Async();
            _currentUser = user;

            return new AuthenticationState(_currentUser);
        }
    }

    private async Task<ClaimsPrincipal> LoginWithAuth0Async()
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
        var loginResult = await _client.LoginAsync();

        if (!loginResult.IsError) {
            authenticatedUser = loginResult.User;
        }
        return authenticatedUser;
    }

    public async void LogOut()
    {
        await _client.LogoutAsync();
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(_currentUser)));
    }
}