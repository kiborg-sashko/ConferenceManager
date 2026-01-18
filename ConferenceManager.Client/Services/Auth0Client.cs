using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;

namespace ConferenceManager.Client.Services;

public class Auth0Client
{
    private readonly OidcClient _oidcClient;

    public Auth0Client()
    {
        _oidcClient = new OidcClient(new OidcClientOptions
        {
            Authority = "dev-myqypevj3wk6nqqb.us.auth0.com",
            ClientId = "tSuKaZBFrFk3gnDsv0SgyMXnXfSsYPDF",
            Scope = "openid profile email",
            RedirectUri = "myapp://callback",
            Browser = new MauiBrowser() // Створимо цей клас нижче
        });
    }

    public async Task<LoginResult> LoginAsync() => await _oidcClient.LoginAsync();
}