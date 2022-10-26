// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Configuration.DependencyInjection;
using Duende.IdentityServer.Configuration.DependencyInjection.BuilderExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityServer.IntegrationTests.Endpoints.Introspection.Setup;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var builder = services.AddIdentityServer(options =>
        {
            options.IssuerUri = "https://idsvr4";
            options.Endpoints.EnableAuthorizeEndpoint = false;
            options.KeyManagement.Enabled = false;
        });

        builder.AddInMemoryClients(Clients.Get());
        builder.AddInMemoryApiResources(Scopes.GetApis());
        builder.AddInMemoryApiScopes(Scopes.GetScopes());
        builder.AddTestUsers(Users.Get());
        builder.AddDeveloperSigningCredential(persistKey: false);
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.UseIdentityServer();
    }
}