// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Configuration.DependencyInjection.Options;
using Duende.IdentityServer.Models.Contexts;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Services.Default;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Stores.Default;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.UnitTests.Common;

internal class MockHttpContextAccessor : IHttpContextAccessor
{
    private HttpContext _context = new DefaultHttpContext();
    public MockAuthenticationService AuthenticationService { get; set; } = new MockAuthenticationService();

    public MockAuthenticationSchemeProvider Schemes { get; set; } = new MockAuthenticationSchemeProvider();

    public MockHttpContextAccessor(
        IdentityServerOptions options = null,
        IUserSession userSession = null,
        IMessageStore<LogoutNotificationContext> endSessionStore = null,
        IServerUrls urls = null)
    {
        options = options ?? TestIdentityServerOptions.Create();

        var services = new ServiceCollection();
        services.AddSingleton(options);

        services.AddSingleton<IAuthenticationSchemeProvider>(Schemes);
        services.AddSingleton<IAuthenticationService>(AuthenticationService);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = Schemes.Default;
        });

        if (userSession == null)
        {
            services.AddScoped<IUserSession, DefaultUserSession>();
        }
        else
        {
            services.AddSingleton(userSession);
        }

        if (endSessionStore == null)
        {
            services.AddTransient<IMessageStore<LogoutNotificationContext>, ProtectedDataMessageStore<LogoutNotificationContext>>();
        }
        else
        {
            services.AddSingleton(endSessionStore);
        }

        if (urls != null)
        {
            services.AddSingleton<IServerUrls>(urls);
        }

        _context.RequestServices = services.BuildServiceProvider();
    }

    public HttpContext HttpContext
    {
        get
        {
            return _context;
        }

        set
        {
            _context = value;
        }
    }
}